using System;
using Android.App;
using Android.Runtime;
using Shiny;

namespace ShinyTest.Droid
{
#if DEBUG
    [Application(Debuggable = true)]
#else
    [Application(Debuggable = false)]
#endif
    public sealed class MainApplication : ShinyAndroidApplication<StartupApp>
    {
        public MainApplication(IntPtr handle, JniHandleOwnership transfer) : base(handle, transfer)
        {
        }
    }
}
