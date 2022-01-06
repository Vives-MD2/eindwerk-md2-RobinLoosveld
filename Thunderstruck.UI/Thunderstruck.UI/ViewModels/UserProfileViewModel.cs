using System.Threading.Tasks;
using Xamarin.Essentials;

namespace Thunderstruck.UI.ViewModels
{
    public class UserProfileViewModel:BaseViewModel
    {
        public string Username { get; set; }

        public UserProfileViewModel()
        {
            ShowUsername();
        }

        public async Task ShowUsername()
        {
            Username = await SecureStorage.GetAsync("CurrentUserUsername");
        }
    }
}