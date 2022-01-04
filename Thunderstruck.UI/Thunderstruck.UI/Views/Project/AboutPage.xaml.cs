using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thunderstruck.DOMAIN.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Thunderstruck.UI.Views.Project
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AboutPage : ContentPage
    {
        public AboutPage()
        {
            InitializeComponent();
        }
        public AboutPage(User user)
        {
            InitializeComponent();
            lblUser.Text = user.Email;
        }
    }
}