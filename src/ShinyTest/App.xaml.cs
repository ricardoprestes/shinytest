using Prism;
using Prism.DryIoc;
using Prism.Ioc;
using ShinyTest.ViewModels;
using ShinyTest.Views;
using Xamarin.Forms;

namespace ShinyTest
{
    public partial class App : Prism.DryIoc.PrismApplication
    {
        protected override IContainerExtension CreateContainerExtension() => PrismContainerExtension.Current;

        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            await NavigationService.NavigateAsync($"{nameof(NavigationPage)}/{nameof(Views.MainPage)}");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainViewModel>();
        }
    }
}
