using System;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;
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
    public class ForecastTodayViewModel : BaseViewModel //GeoJsonConverterFactory
    {
        //using Geolocation to get the current phone location coordinates
        //Source:  https://docs.microsoft.com/en-us/xamarin/essentials/geolocation?context=xamarin%2Fandroid&tabs=android
        //using Geocoding to get coordinates by placename
        //Source: https://docs.microsoft.com/en-us/xamarin/essentials/geocoding?tabs=android

        private PageService _pageService = new PageService();

        private HttpClient _httpClient;

        private string _enteredLocation;

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

        private Location _currentPhoneLocation;
        public Location CurrentPhoneLocation
        {
            get => _currentPhoneLocation;
            set
            {
                if (value == _currentPhoneLocation) return;
                _currentPhoneLocation = value;
                OnPropertyChanged();
            }
        }

        public ICommand GetCurrentWeatherCommand { get; set; }
        public ICommand GetCurrentLocationCommand { get; set; }

        public ForecastTodayViewModel()
        {
            GetCurrentWeatherCommand = new Command(async x => await GetCurrentWeatherByLocationText());
            GetCurrentLocationCommand = new Command(async x => await GetCurrentLocation());
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
                    //AutoConvert json to classes: Visual Studio -> Edit -> Paste Special -> "Paste JSON as Classes"
                    response.EnsureSuccessStatusCode();
                    var stream = await response.Content.ReadAsStreamAsync();
                    var result = await JsonSerializer.DeserializeAsync<CurrentWeatherModelRoot>(stream);
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
            CurrentPhoneLocation = await Geolocation.GetLocationAsync();
            using (ApiService<ILocationDataApi> service =
                   new ApiService<ILocationDataApi>(GlobalVars.ThunderstruckApiOnline))
            {

                var locationData = new LocationData
                    { Location = LocationDataHelper.ConvertLocationToPoint(CurrentPhoneLocation), TimeStamp = DateTime.Now };
                //get placename etc to use in the db

                var placemark = await LocationDataHelper.GetPlacemarkByCoordinates(CurrentPhoneLocation.Latitude, CurrentPhoneLocation.Longitude);
                LocationDataWithDouble locationMapObject = new LocationDataWithDouble
                {
                    LocationName = placemark.Locality,
                    TimeStamp = locationData.TimeStamp,
                    XLongitude = locationData.Location.X,
                    YLatitude = locationData.Location.Y
                };
                var response = await service.myService.Create(locationMapObject);
                var test = await service.myService.GetById(8);
                //var convertedObject =  JsonConvert.DefaultSettings..DeserializeObject<ApiSingleResponse<LocationData>>(response)?.Value;
            }
        }
        private async Task<Location> GetCoordinatesByPlaceName(string placeName)
        {
            if (!string.IsNullOrWhiteSpace(EnteredLocation))
            {
                var locations = await Geocoding.GetLocationsAsync(placeName);
                var location = locations?.FirstOrDefault();
                if (location != null)
                {
                    Debug.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
                }

                return location;
            }
            return null;
        }
    }
}