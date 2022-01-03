using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Newtonsoft.Json;
using Thunderstruck.UI.AppService;
using Thunderstruck.UI.AuthenticationModels;
using Thunderstruck.UI.AuthenticationModels.CurrentWeatherModel;
using Utf8Json;
using Xamarin.Forms;
using JsonSerializer = Utf8Json.JsonSerializer;

namespace Thunderstruck.UI.ViewModels
{
    public class ForecastTodayViewModel:BaseViewModel
    {
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

        public ForecastTodayViewModel()
        {
            GetCurrentWeatherCommand = new Command(async x => await GetCurrentWeatherByLocationText());
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
                    var result = await JsonSerializer.DeserializeAsync<CurrentWeatherModel.CurrentWeatherRootModel>(stream);
                    Console.WriteLine(stream.ToString());
                    //await Application.Current.MainPage.DisplayAlert("Alert", result, "Ok");
                }
                catch (JsonParsingException ex)
                {
                    Console.WriteLine(ex.Message);
                    throw;
                }

            }
        }
    }
}