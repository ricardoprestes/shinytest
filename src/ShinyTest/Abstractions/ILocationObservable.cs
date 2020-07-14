using System;
using Shiny.Locations;

namespace ShinyTest.Abstractions
{
    public interface ILocationObservable : IObservable<string>
    {
        void OnLocationChanged(IGpsReading reading);
    }
}
