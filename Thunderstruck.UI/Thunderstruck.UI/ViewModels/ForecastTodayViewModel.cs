using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using NetTopologySuite.IO.Converters;
using Newtonsoft.Json;
using Thunderstruck.DOMAIN.Contracts;
using Thunderstruck.DOMAIN.Models;
using Thunderstruck.UI.Api;
using Thunderstruck.UI.Api.Contracts;
using Thunderstruck.UI.AppService;
using Thunderstruck.UI.Helpers;
using Thunderstruck.UI.ResponseModels.WeatherModels;
using Thunderstruck.UI.Views.Utils;
using Utf8Json;
using Xamarin.Essentials;
using Xamarin.Forms;
using JsonConverter = Newtonsoft.Json.JsonConverter;
using JsonSerializer = Utf8Json.JsonSerializer;

namespace Thunderstruck.UI.ViewModels
{
    public class ForecastTodayViewModel : BaseViewModel //GeoJsonConverterFactory
    {
        //using Geolocation
        //Source:  https://docs.microsoft.com/en-us/xamarin/essentials/geolocation?context=xamarin%2Fandroid&tabs=android

        private string _enteredLocation;

        private PageService _pageService = new PageService();

        //
        private HttpClient _httpClient;

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
        public ICommand GetCurrentLocationCommand { get; set; }

        //info: https://docs.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken?view=net-6.0


        public ForecastTodayViewModel()
        {
            GetCurrentWeatherCommand = new Command(async x => await GetCurrentWeatherByLocationText());
            GetCurrentLocationCommand = new Command(async x => await GetCurrentLocation());
            //GetCurrentLocation();
        }

        private async Task GetCurrentWeatherByLocationText()
        {

            var client = new HttpClient();

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri =
                    new Uri(
                        $"https://community-open-weather-map.p.rapidapi.com/weather?q={EnteredLocation}&units=metric"),
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
            //try
            // {

            //var request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));
            //Cts = new CancellationTokenSource();
            //var location = await Geolocation.GetLocationAsync(request, Cts.Token);
            //var location = await Geolocation.GetLocationAsync();
            //if (location != null)
            //{

            //    Debug.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
            //var locationData = new LocationData { Location = LocationDataHelper.ConvertLocationToPoint(location), TimeStamp = DateTime.Now };

            //Point mapping
            //(3.24, 50.83) };

            var location = await Geolocation.GetLocationAsync();
            using (ApiService<ILocationDataApi> service =
                   new ApiService<ILocationDataApi>(GlobalVars.ThunderstruckApiOnline))
            {
                var locationData = new LocationData
                    { Location = LocationDataHelper.ConvertLocationToPoint(location), TimeStamp = DateTime.Now };
                LocationDataWithDouble locationMapObject = new LocationDataWithDouble
                {
                    LocationName = "Test1",
                    TimeStamp = locationData.TimeStamp,
                    XLongitude = locationData.Location.X,
                    YLatitude = locationData.Location.Y
                };
                var response = await service.myService.Create(locationMapObject);
                var test = await service.myService.GetById(8);
                var test2 = "hi there";
                //var convertedObject =  JsonConvert.DefaultSettings..DeserializeObject<ApiSingleResponse<LocationData>>(response)?.Value;
            }

            //_httpClient = new HttpClient();
            //_httpClient.BaseAddress = new Uri("https://api.spotify.com/v1/me");

            //The token needs to be added as a header for this to work
            //var httpResponseMessage = await _httpClient.PostAsync()

            //private async Task AddCurrentLocationToDb(LocationData locationData)
            //{
            //    using ApiService<ILocationDataApi> service = new ApiService<ILocationDataApi>(GlobalVars.ThunderstruckApiOnline);
            //    {

            //    }
            //    await service.myService.Create(locationData);
            //}
        }
    }
}