using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Thunderstruck.UI.ViewModels.User
{
    public class UserProfileViewModel:BaseViewModel
    {
        private string _email;
        private string _username;

        public string Email
        {
            get => _email;
            set => SetValue(ref _email, value);
        }

        public string Username
        {
            get => _username;
            set => SetValue(ref _username, value);
        }
        //commands
        public ICommand ShowUserInfoCommand { get; set; }
        public UserProfileViewModel()
        {
            ShowUserInfoCommand = new Command(async x => await ShowUserInfo());
        }

        public async Task ShowUserInfo()
        {
            Username = await SecureStorage.GetAsync("CurrentUserUsername");
            Email = await SecureStorage.GetAsync("CurrentUserEmail");
        }
    }
}