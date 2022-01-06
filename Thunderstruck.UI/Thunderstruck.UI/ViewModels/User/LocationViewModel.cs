using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Newtonsoft.Json;
using Thunderstruck.DOMAIN.Models;
using Thunderstruck.UI.Api;
using Thunderstruck.UI.Api.Contracts;
using Thunderstruck.UI.AppService;
using Thunderstruck.UI.ResponseModels.WeatherModels;
using Thunderstruck.UI.Views.Utils;
using Xamarin.Forms;

namespace Thunderstruck.UI.ViewModels.User
{
    public struct GetParam
    {
        public int skip;
        public int take;
    }
    public class LocationViewModel : BaseViewModel
    {
        private PageService pageService = new PageService();
        private ObservableCollection<LocationDataWithDouble> _locations;

        public ObservableCollection<LocationDataWithDouble> Locations
        {
            get => _locations;
            set
            {
                if (value == _locations) return;
                _locations = value;
                OnPropertyChanged();
            }
        }
        // Commands
        public ICommand GetAllLocationsCommand { get; set; }

        public LocationViewModel()
        {
            _locations = new ObservableCollection<LocationDataWithDouble>();
            GetAllLocationsCommand = new Command<GetParam>(async x => await GetAllLocations(x));
        }

        private async Task GetAllLocations(GetParam x)
        {
            try
            {
                using (ApiService<ILocationDataApi> service =
                       new ApiService<ILocationDataApi>(GlobalVars.ThunderstruckApiOnline))
                {
                    var response = await service.myService.Get(x.skip, x.take);
                    var test = "hi";
                    IEnumerable<LocationDataWithDouble> locations =
                        JsonConvert.DeserializeObject<ApiMultiResponse<LocationDataWithDouble>>(response)?.Value;

                    foreach (var location in locations)
                    {
                        Locations.Add(location);
                    }
                }
            }
            catch (Exception ex)
            {
                await pageService.PushAsync(new ErrorPage(ex));
            }
        }
    }
}

