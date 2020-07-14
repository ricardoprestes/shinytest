using Microsoft.Extensions.DependencyInjection;
using Prism.DryIoc;
using Shiny;
using Shiny.Prism;
using ShinyTest.Abstractions;
using ShinyTest.Services;

namespace ShinyTest
{
    public class StartupApp : PrismStartup
    {
        public StartupApp() : base(PrismContainerExtension.Current)
        {
        }

        protected override void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<ILocationObservable, LocationObservable>();
            services.UseGps<LocationDelegate>();
        }
    }
}
