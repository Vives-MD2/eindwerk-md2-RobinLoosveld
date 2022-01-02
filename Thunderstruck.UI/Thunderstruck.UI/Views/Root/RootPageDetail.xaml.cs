using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpotifyAPI.Web;
using Thunderstruck.DOMAIN.Models;
using Thunderstruck.UI.AuthenticationModels;
using Thunderstruck.UI.Views.Project;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Thunderstruck.UI.Views.Root
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RootPageDetail : ContentPage
    {
        private JsonSerializer _serializer = new JsonSerializer();
        private string _clientId = "f3fa527a095f46d0a1b70a344978a5d5";
        //private string _clientSecret = "6e31e86358e843b69cd07ff15139376a";

        private string _authenticationUrl = "http://user21.vivesxamarin.com/xamarinauth/";
        private string _callback = "http://user21.vivesxamarin.com/callback";
        private string _AuthToken;

        public string AuthToken
        {
            get => _AuthToken;
            set
            {
                if (value == _AuthToken) return;
                _AuthToken = value;
                OnPropertyChanged();
            }
        }

        public RootPageDetail()
        {
            InitializeComponent();
        }

        private async void BtnLogin_OnClicked(object sender, EventArgs e)
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
                    //I need to specify what Spotify scopes I need access to. They need to be separated by a single space.
                    //user-read-currently-playing
                    //user-read-email
                    //app-remote-control
                    //playlist-modify-private
                    //playlist-modify-public
                    //playlist-modify-private
                    var scopes =
                        "user-read-private " +
                        "user-read-email " +
                        "playlist-modify-private " +
                        "user-read-currently-playing " +
                        "app-remote-control " +
                        "playlist-modify-public ";
                   //this doesn't specify the required scopes but does actually seem to redirect correctly
                    var authUrl = new Uri(_authenticationUrl + scheme); 
                    
                    //this returns a url containing a token
                   var authUrlSpotify = new Uri(
                        "https://accounts.spotify.com/authorize?response_type=token" +
                        $"&client_id={_clientId}" +
                        $"&scope={scopes}" +
                        $"&redirect_uri={_callback}");

                    var callbackUrl = new Uri("xamarinessentials://");
                    //await App.Current.MainPage.DisplayAlert("Alert", callbackUrl.ToString(), "Ok");
                    r = await WebAuthenticator.AuthenticateAsync(authUrl, callbackUrl);
                }

                AuthToken = r?.AccessToken ?? r?.IdToken;

                GetUserInfoUsingToken(AuthToken);
                GetPublicPlaylists(AuthToken);
            }
            catch (Exception ex)
            {
                AuthToken = string.Empty;

                await App.Current.MainPage.DisplayAlert("Alert", ex.Message, "Ok");
            }
        }

        private void GetPublicPlaylists(string authToken)
        {
            HttpClient httpClient = new HttpClient();
        }

        private async void GetUserInfoUsingToken(string authToken)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://api.spotify.com/v1/me");
            var httpResponseMessage = await httpClient.GetAsync("tokeninfo?access_token=" + authToken);
            using (var stream = await httpResponseMessage.Content.ReadAsStreamAsync())
            using (var reader = new StreamReader(stream))
            using (var json = new JsonTextReader(reader))
            {
                var jsoncontent = _serializer.Deserialize<SpotifyProfileResponse>(json);
                await SecureStorage.SetAsync("UserToken", authToken);
                //Not the best way to save auth token and check if authtoken has expired instead try implementing refresh token
                await App.Current.MainPage.DisplayAlert("It Worked?", jsoncontent.email, "Ok");

                await Navigation.PushAsync(new AboutPage(new User { Email = jsoncontent.email }));
            }
        }
        //private async Task CallTest()
        //{
        //    var spotify = new SpotifyClient("BQClaCkNLcd3RYLMZpueTQFNy6lxgbHPyLfCjpwHrhp7FIk9GDx6yZVfOElHH9XW-8OM0Cl8MPLohhgAc-6lGLOJ5BY3ZS-TZibnwfYtqXYCBNOOniti8KXwYFbCA5t2bA3fd-FaswqxeYkhv5jOW9trdrNFHlIcE-ejMnGtHfT5J5QtIOUeinKT0CgAYcMkLFfbv9Hc");
        //    var track = await spotify.Tracks.Get("1s6ux0lNiTziSrd7iUAADH");
        //    lblLogin.Text = track.Name;
        //}

        //private async Task LoginTest()
        //{
        //    var loginRequest = new LoginRequest(new Uri("http://localhost:5000"),
        //        _clientId, LoginRequest.ResponseType.Code);

        //    //Specify the Authorization scopes needed in the app
        //    //https://developer.spotify.com/documentation/general/guides/authorization/scopes/
        //    loginRequest.Scope = new List<string>()
        //    {
        //        Scopes.UserReadCurrentlyPlaying,
        //        Scopes.UserReadEmail,
        //        Scopes.AppRemoteControl,
        //        Scopes.PlaylistModifyPrivate,
        //        Scopes.PlaylistModifyPublic,
        //        Scopes.PlaylistModifyPrivate
        //    };
        //    var uri = loginRequest.ToUri();

        //}

        //public async Task GetCallback(string code)
        //{
        //    var response = await new OAuthClient()
        //        .RequestToken(new AuthorizationCodeTokenRequest(_clientId, _clientSecret, code, new Uri("http://localhost:5000")));
        //    var spotify = new SpotifyClient(response.AccessToken);  // Also important for later: response.RefreshToken
        //}
    }
}