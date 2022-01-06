using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thunderstruck.DOMAIN.Models;
using Thunderstruck.UI.ViewModels;
using Thunderstruck.UI.ViewModels.User;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Thunderstruck.UI.Views.Project.UserInfo
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserProfilePage : ContentPage
    {
        
        public UserProfilePage(User user)
        {
            InitializeComponent();
            BindingContext = new UserProfileViewModel();
            
        }
    }
}