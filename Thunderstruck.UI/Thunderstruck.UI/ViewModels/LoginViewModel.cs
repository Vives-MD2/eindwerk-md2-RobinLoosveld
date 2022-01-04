using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using SpotifyAPI.Web;
using Thunderstruck.DOMAIN.Models;
using Thunderstruck.UI.Api;
using Thunderstruck.UI.Api.Contracts;
using Thunderstruck.UI.ApiModels.UserModels;
using Thunderstruck.UI.AppService;
using Thunderstruck.UI.AppService.Contracts;
using Thunderstruck.UI.Helpers;
using Thunderstruck.UI.Views.Project;
using Thunderstruck.UI.Views.Project.UserInfo;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Thunderstruck.UI.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        //private string _clientId = "f3fa527a095f46d0a1b70a344978a5d5";
        private string _authenticationUrl = "http://user21.vivesxamarin.com/xamarinauth/";
       
        private JsonSerializer _serializer = new JsonSerializer();
        private PageService _pageService = new PageService();

        //commands
        public ICommand LoginSpotifyCommand { get; set; }
        private SpotifyUserRootModel _currentUser { get; set; }

        public SpotifyUserRootModel CurrentUser
        {
            get=>_currentUser;
            set
            {
                if (value == _currentUser) return;
                {
                    _currentUser = value;
                    OnPropertyChanged();
                }
            }
        }

        public LoginViewModel()
        {
            //to bind to the login button
            LoginSpotifyCommand = new Command(async x=> await LoginSpotify());
            CurrentUser = new SpotifyUserRootModel();
        }

        private string _authToken;
        public string AuthToken
        {
            get => _authToken;
            set
            {
                if (value == _authToken) return;
                _authToken = value;
                OnPropertyChanged();
            }
        }
        private async Task LoginSpotify()
        {
            Location test = await Geolocation.GetLocationAsync();
            string scheme = "Spotify";
            try
            {
                WebAuthenticatorResult r = null;

                if (scheme.Equals("Apple")
                    && DeviceInfo.Platform == DevicePlatform.iOS
                    && DeviceInfo.Version.Major >= 13)
                {
                    r = await AppleSignInAuthenticator.AuthenticateAsync();
                }
                else if (scheme.Equals("Spotify"))
                {
                    //this doesn't specify the required scopes but does actually seem to redirect correctly
                    var authUrl = new Uri(_authenticationUrl + scheme);
                    var callbackUrl = new Uri("xamarinessentials://");
                    //await App.Current.MainPage.DisplayAlert("Alert", callbackUrl.ToString(), "Ok");
                    r = await WebAuthenticator.AuthenticateAsync(authUrl, callbackUrl);
                }

                AuthToken = r?.AccessToken ?? r?.IdToken;
                var refreshToken = r.RefreshToken;
                var timestamp = r.Timestamp;

                GetUserInfoUsingToken(AuthToken);
                //GetPublicPlaylists(AuthToken);
            }
            catch (Exception ex)
            {
                AuthToken = string.Empty;

                await App.Current.MainPage.DisplayAlert("Alert", ex.Message, "Ok");
            }
        }
        //private void GetPublicPlaylists(string authToken)
        //{
        //    HttpClient httpClient = new HttpClient();
        //}
        private async void GetUserInfoUsingToken(string authToken)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://api.spotify.com/v1/me");
            //The token needs to be added as a header for this to wordk
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",authToken);
            var httpResponseMessage = await httpClient.GetAsync("https://api.spotify.com/v1/me");

            using (var stream = await httpResponseMessage.Content.ReadAsStreamAsync())
            using (var reader = new StreamReader(stream))
            using (var json = new JsonTextReader(reader))
            {
                CurrentUser = _serializer.Deserialize<SpotifyUserRootModel>(json);
                await SecureStorage.SetAsync("UserToken", authToken);
                //Not the best way to save auth token and check if authtoken has expired instead try implementing refresh token
                await App.Current.MainPage.DisplayAlert("It Worked?", CurrentUser.email + CurrentUser.id, "Ok");

                await _pageService.PushAsync(new UserProfilePage(new User { Email = CurrentUser.email }));
            }
            //user the user info to add a user to the db
            using (ApiService<IUserApi> service = new ApiService<IUserApi>(GlobalVars.ThunderstruckApiOnline))
            {
                var dbUser = UserHelper.MapSpotifyUserToDbUser(CurrentUser);
            }
        }

    }
}
