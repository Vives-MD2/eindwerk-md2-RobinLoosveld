using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Refit;
using Thunderstruck.UI.ViewModels;
using Thunderstruck.UI.ViewModels.Forecast;
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
    }
}