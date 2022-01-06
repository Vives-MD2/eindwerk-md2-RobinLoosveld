using Thunderstruck.DOMAIN.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Thunderstruck.UI.Views.Project.UserInfo
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserTabbedPage : TabbedPage
    {
        public UserTabbedPage()
        {
            InitializeComponent();
            this.Children.Add(new UserProfilePage(new User()) { Title = "Profile" });
            this.Children.Add(new LocationPage(){Title = "Past Locations"});
            this.Children.Add(new AchievementPage() {Title = "Achievements"});
        }
    }
}