
using Microsoft.Maui.Devices;

namespace EIIBD
{
    public partial class App : Application
    {
        public App(AppShell appShell)
        {
            InitializeComponent();

#if WINDOWS
            _appShell = appShell;
#endif
#if ANDROID
            MainPage = appShell;
#endif
        }

#if WINDOWS
        AppShell _appShell;
        protected override Window CreateWindow(IActivationState? activationState) =>
        new Window(_appShell)
            {
                Width = 376*0.8, //360
                Height = 824.67*0.8, //736.67
                X = 1300,
                Y = 100,
            };
#endif
    }
}
