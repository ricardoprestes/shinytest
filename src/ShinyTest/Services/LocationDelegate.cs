using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Shiny.Locations;
using ShinyTest.Abstractions;

namespace ShinyTest.Services
{
    public class LocationDelegate : IGpsDelegate
    {
        private readonly ILocationObservable _locationObserver;

        public LocationDelegate(ILocationObservable locationObserver)
        {
            _locationObserver = locationObserver;
        }

        public Task OnReading(IGpsReading reading)
        {
            if (reading?.Position != null)
                _locationObserver.OnLocationChanged(reading);

            return Task.CompletedTask;
        }
    }

    public sealed class LocationObservable : ILocationObservable
    {
        private readonly IList<IObserver<string>> _observers = new List<IObserver<string>>();

        public void OnLocationChanged(IGpsReading reading)
        {
            Parallel.ForEach(_observers, (o) => o.OnNext($"Latitude: {reading.Position.Latitude} | Longitude: {reading.Position.Longitude}"));
        }

        public IDisposable Subscribe(IObserver<string> newObserver)
        {
            if (!_observers.Contains(newObserver))
                _observers.Add(newObserver);

            return new ObserverDisposer(_observers, newObserver);
        }
    }

    public sealed class ObserverDisposer : IDisposable
    {
        private readonly IObserver<string> _observer;
        private readonly IList<IObserver<string>> _observersPersitent;

        public ObserverDisposer(IList<IObserver<string>> observersPersitent,
            IObserver<string> observer)
        {
            _observer = observer;
            _observersPersitent = observersPersitent;
        }

        public void Dispose()
        {
            if (_observersPersitent.Contains(_observer))
                _observersPersitent.Remove(_observer);

            GC.SuppressFinalize(this);
        }
    }
}
