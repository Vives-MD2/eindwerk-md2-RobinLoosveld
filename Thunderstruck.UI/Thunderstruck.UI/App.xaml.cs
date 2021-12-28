using System;
using Thunderstruck.UI.Views.Root;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Thunderstruck.UI
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new RootPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
