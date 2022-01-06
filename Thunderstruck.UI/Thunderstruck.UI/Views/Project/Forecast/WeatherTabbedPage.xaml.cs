using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Thunderstruck.UI.Views.Project.Forecast
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WeatherTabbedPage : TabbedPage
    {
        public WeatherTabbedPage()
        {
            //add icons to pages
            //
            var forcastTodayPage = new ForecastTodayPage();
            forcastTodayPage.IconImageSource = "calendar.svg";
            forcastTodayPage.Title = "Today";

            InitializeComponent();
            this.Children.Add(new ForecastTodayPage(){Title="Today"});
            this.Children.Add(new ForecastTomorrowPage(){Title="Tomorrow"});
            this.Children.Add(new ForecastWeekPage(){Title="Next 5 Days"});
        }
    }
}