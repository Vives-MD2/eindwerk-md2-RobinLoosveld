using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Windows.Input;
using Newtonsoft.Json;
using Thunderstruck.UI.Api;
using Thunderstruck.UI.Api.Contracts;
using Thunderstruck.UI.AppService;
using Thunderstruck.UI.Helpers;
using Thunderstruck.UI.ResponseModels.UserModels;
using Thunderstruck.UI.Views.Project.Forecast;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Thunderstruck.UI.ViewModels.User
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
            get => _currentUser;
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
            LoginSpotifyCommand = new Command(async x => await LoginSpotify());
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

        private string _refreshToken;

        public string RefreshToken
        {
            get => _refreshToken;
            set
            {
                if (value == _authToken) return;
                _authToken = value;
                OnPropertyChanged();
            }
        }
        private async Task LoginSpotify()
        {
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
                    var authUrl = new Uri(_authenticationUrl + scheme);
                    var callbackUrl = new Uri("xamarinessentials://");
                    r = await WebAuthenticator.AuthenticateAsync(authUrl, callbackUrl);
                }

                AuthToken = r?.AccessToken ?? r?.IdToken;
                var refreshToken = r.RefreshToken;
                var timestamp = r.Timestamp;

                //check if token had expired
                if (await UserHelper.CheckIfTokenHasExpired(timestamp, AuthToken))
                {
                    await App.Current.MainPage.DisplayAlert("Alert", "Access token has expired", "ok");
                }

                GetUserInfoUsingToken(AuthToken);
                //GetPublicPlaylists(AuthToken);
            }
            catch (Exception ex)
            {
                AuthToken = string.Empty;
                await App.Current.MainPage.DisplayAlert("Alert", ex.Message, "Ok");
            }
        }
        private async void GetUserInfoUsingToken(string authToken)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://api.spotify.com/v1/me");

            //The token needs to be added as a header for this to work
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);
            var httpResponseMessage = await httpClient.GetAsync("https://api.spotify.com/v1/me");

            using (var stream = await httpResponseMessage.Content.ReadAsStreamAsync())
            using (var reader = new StreamReader(stream))
            using (var json = new JsonTextReader(reader))
            {
                CurrentUser = _serializer.Deserialize<SpotifyUserRootModel>(json);
                if (await SecureStorage.GetAsync("CurrentUserUsername") != CurrentUser.display_name)
                {
                    SecureStorage.Remove("CurrentUserUsername");
                    await SecureStorage.SetAsync("CurrentUserUsername", CurrentUser.display_name);
                }
                if (await SecureStorage.GetAsync("CurrentUserEmail") != CurrentUser.email)
                {
                    SecureStorage.Remove("CurrentUserEmail");
                    await SecureStorage.SetAsync("CurrentUserEmail", CurrentUser.email);
                }

                await SecureStorage.SetAsync("UserToken", authToken);
                //Not the best way to save auth token and check if authtoken has expired instead try implementing refresh token
                await App.Current.MainPage.DisplayAlert("It Worked?", CurrentUser.email + CurrentUser.id, "Ok");
            }
            //use the spotifyuser info to add a user to the db
            using (ApiService<IUserApi> service = new ApiService<IUserApi>(GlobalVars.ThunderstruckApiOnline))
            {
                var response = await service.myService.GetByEmail(CurrentUser.email);
                var emailInDbUser = JsonConvert.DeserializeObject<ApiSingleResponse<DOMAIN.Models.User>>(response).Value;

                //do not create db user of there're already one with the same email
                if (CurrentUser.email != emailInDbUser.Email)
                {
                    var dbUser = await UserHelper.MapSpotifyUserToDbUser(CurrentUser);
                    await service.myService.Create(dbUser);
                }

                //create new stack
                await _pageService.PushModelAsync(new WeatherTabbedPage());

            }
        }

    }
}
