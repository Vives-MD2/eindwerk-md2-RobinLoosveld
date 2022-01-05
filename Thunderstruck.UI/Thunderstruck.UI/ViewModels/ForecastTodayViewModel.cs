using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Newtonsoft.Json;
using Thunderstruck.DOMAIN.Contracts;
using Thunderstruck.DOMAIN.Models;
using Thunderstruck.UI.Api;
using Thunderstruck.UI.Api.Contracts;
using Thunderstruck.UI.AppService;
using Thunderstruck.UI.Helpers;
using Thunderstruck.UI.ResponseModels.WeatherModels;
using Utf8Json;
using Xamarin.Essentials;
using Xamarin.Forms;
using JsonSerializer = Utf8Json.JsonSerializer;

namespace Thunderstruck.UI.ViewModels
{
    public class ForecastTodayViewModel : BaseViewModel
    {
        //using Geolocation
        //Source:  https://docs.microsoft.com/en-us/xamarin/essentials/geolocation?context=xamarin%2Fandroid&tabs=android

        private string _enteredLocation;
        private PageService _pageService = new PageService();

        public string EnteredLocation
        {
            get => _enteredLocation;
            set
            {
                if (value == _enteredLocation) return;
                _enteredLocation = value;
                OnPropertyChanged();
            }
        }
        public ICommand GetCurrentWeatherCommand { get; set; }
        //info: https://docs.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken?view=net-6.0
        public CancellationTokenSource Cts { get; set; }


        public ForecastTodayViewModel()
        {
            GetCurrentWeatherCommand = new Command(async x => await GetCurrentWeatherByLocationText());
            GetCurrentLocation();
        }
        private async Task GetCurrentWeatherByLocationText()
        {

            var client = new HttpClient();

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri =
                    new Uri($"https://community-open-weather-map.p.rapidapi.com/weather?q={EnteredLocation}&units=metric"),
                Headers =
                {
                    { "x-rapidapi-host", "community-open-weather-map.p.rapidapi.com" },
                    { "x-rapidapi-key", "849c053122mshb66d0696c8175cbp14f39djsn5849b90c83d1" },
                },
            };
            using (var response = await client.SendAsync(request))
            {
                try
                {
                    //create model for deserialization
                    //check online voor een json to model converter
                    response.EnsureSuccessStatusCode();
                    var stream = await response.Content.ReadAsStreamAsync();
                    var result = await JsonSerializer.DeserializeAsync<CurrentWeatherModelRoot>(stream);
                    //Console.WriteLine(result.rain._1h);
                    await Application.Current.MainPage.DisplayAlert("Alert", result.name + result.coord.lat, "Ok");
                }
                catch (JsonParsingException ex)
                {
                    Console.WriteLine(ex.Message);
                    throw;
                }

            }
        }

        private async Task GetCurrentLocation()
        {
            try
            {
                var request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));
                Cts = new CancellationTokenSource();
                var location = await Geolocation.GetLocationAsync(request, Cts.Token);

                if (location != null)
                {
                    Debug.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
                    var locationData = new LocationData { Location = LocationDataHelper.ConvertLocationToPoint(location), TimeStamp = DateTime.UtcNow };
                    await AddCurrentLocationToDb(locationData);
                }

                Point test = new Point { };
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Handle not supported on device exception
            }
            catch (FeatureNotEnabledException fneEx)
            {
                // Handle not enabled on device exception
            }
            catch (PermissionException pEx)
            {
                // Handle permission exception
            }
            catch (Exception ex)
            {
                // Unable to get location
            }
        }
        private async Task AddCurrentLocationToDb(LocationData locationData)
        {
            using ApiService<ILocationDataApi> service = new ApiService<ILocationDataApi>(GlobalVars.ThunderstruckApiOnline);
            await service.myService.Create(locationData);
        }
    }
}