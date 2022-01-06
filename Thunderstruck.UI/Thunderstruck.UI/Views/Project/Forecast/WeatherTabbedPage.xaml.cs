using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Thunderstruck.UI.Views.Project.Forecast
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WeatherTabbedPage : TabbedPage
    {
        public WeatherTabbedPage()
        {

            InitializeComponent();
            //add icons to pages
            //
            var forecastTodayPage = new ForecastTodayPage();
            forecastTodayPage.IconImageSource = "forecast_today.png";
            forecastTodayPage.Title = "Today";

            var forecastTomorrowPage = new ForecastTomorrowPage();
            forecastTomorrowPage.IconImageSource = "forecast_tomorrow.png";
            forecastTomorrowPage.Title = "Tomorrow";

            var forecastWeekPage = new ForecastWeekPage();
            forecastWeekPage.IconImageSource = "forecast_week.png";
            forecastWeekPage.Title = "Next 5 days";

            this.Children.Add(forecastTodayPage);
            this.Children.Add(forecastTomorrowPage);
            this.Children.Add(forecastWeekPage);
        }
    }
}