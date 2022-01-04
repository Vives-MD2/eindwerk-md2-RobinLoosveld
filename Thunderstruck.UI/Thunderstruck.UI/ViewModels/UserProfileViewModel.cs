using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

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