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
            this.Children.Add(new ForecastTodayPage(){Title="Today"});
            this.Children.Add(new ForecastTomorrowPage(){Title="Tomorrow"});
            this.Children.Add(new ForecastWeekPage(){Title="Next 5 Days"});
        }
    }
}