using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Refit;
using Thunderstruck.UI.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Thunderstruck.UI.Views.Project.Forecast
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class ForecastTodayPage : ContentPage
    {

        public ForecastTodayPage()
        {
            InitializeComponent();
            BindingContext = new ForecastTodayViewModel();
        }
        private async void BtnToday_OnClicked(object sender, EventArgs e)
        { 
            //get current weather
            // await GetCurrentWeatherByLocationText();
            if (eEnterLocation.Text is null)
            {
                await Application.Current.MainPage.DisplayAlert("Alert", "Please enter a valid location and try again.", "Ok");
            }
        }

    }
}