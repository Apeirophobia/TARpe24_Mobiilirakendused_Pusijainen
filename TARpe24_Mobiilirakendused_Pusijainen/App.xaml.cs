using Microsoft.Extensions.DependencyInjection;

namespace TARpe24_Mobiilirakendused_Pusijainen
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            var startPage = new StartPage();

            var navPage = new NavigationPage(startPage)
            {
                BarBackgroundColor = Colors.GreenYellow,
                BarTextColor = Colors.Black
            };

            return new Window(navPage);
        }
    }
}