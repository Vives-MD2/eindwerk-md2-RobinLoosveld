using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thunderstruck.UI.Views.Project.Forecast;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Thunderstruck.UI.Views.Project
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WeatherPage : TabbedPage
    {
        public WeatherPage()
        {
            InitializeComponent();
            this.Children.Add(new ForecastToday(){Title="Today"});
            this.Children.Add(new ForecastTomorrow(){Title="Tomorrow"});
            this.Children.Add(new ForecastWeek(){Title="Next 5 Days"});
        }
    }
}