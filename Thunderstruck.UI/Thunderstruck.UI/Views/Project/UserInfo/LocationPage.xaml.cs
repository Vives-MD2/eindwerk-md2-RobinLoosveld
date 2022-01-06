using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thunderstruck.UI.ViewModels.User;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Thunderstruck.UI.Views.Project.UserInfo
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LocationPage : ContentPage
    {
        private GetParam _skipTake;
        public LocationPage()
        {
            InitializeComponent();
            _skipTake = new GetParam() { skip = 0, take = 10 };
            (BindingContext as LocationViewModel).GetAllLocationsCommand.Execute(_skipTake);
            
        }
    }
}