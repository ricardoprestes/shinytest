using System;
using Prism.Navigation;
using Shiny.Locations;
using ShinyTest.Abstractions;

namespace ShinyTest.ViewModels
{
    public class MainViewModel : BaseViewModel, IObserver<string>
    {
        private readonly IGpsManager _gpsManager;
        private readonly IDisposable _locationObservableDisposer;

        private string _currentLocation;
        public string CurrentLocation
        {
            get => _currentLocation;
            set => SetProperty(ref _currentLocation, value);
        }

        public MainViewModel(INavigationService navigationService,
                             IGpsManager gpsManager,
                             ILocationObservable locationObservable) :
            base(navigationService)
        {
            Title = "Shiny";
            CurrentLocation = "No location found..";
            _locationObservableDisposer = locationObservable.Subscribe(this);
            _gpsManager = gpsManager;
        }

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            if (_gpsManager.IsListening)
                await _gpsManager.StopListener();

            await _gpsManager.StartListener(BuildRequest());

            base.OnNavigatedTo(parameters);
        }

        public override void Destroy()
        {
            _locationObservableDisposer.Dispose();
            base.Destroy();
        }

        private static GpsRequest BuildRequest()
        {
            return new GpsRequest
            {
                UseBackground = true,
                Priority = GpsPriority.Highest,
                Interval = TimeSpan.FromSeconds(5),
                ThrottledInterval = TimeSpan.FromSeconds(3)
            };
        }

        public void OnCompleted()
        {
        }

        public void OnError(Exception error)
        {
            CurrentLocation = error.Message;
        }

        public void OnNext(string value)
        {
            CurrentLocation = value;
        }
    }
}
