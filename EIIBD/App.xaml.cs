
using Microsoft.Maui.Devices;

namespace EIIBD
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

#if ANDROID
            MainPage = new AppShell();
#endif
        }

#if WINDOWS
        protected override Window CreateWindow(IActivationState? activationState) =>
        new Window(new AppShell())
            {
                Width = 376*0.8, //360
                Height = 824.67*0.8, //736.67
                X = 1300,
                Y = 100,
            };
#endif
    }
}
