using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Thunderstruck.DOMAIN.Models;
using Thunderstruck.UI.Views.Project;
using Thunderstruck.UI.Views.Project.Forecast;
using Thunderstruck.UI.Views.Project.Music;
using Thunderstruck.UI.Views.Project.UserInfo;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Thunderstruck.UI.Views.Root
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RootPageFlyout : ContentPage
    {
        public ListView ListView;

        public RootPageFlyout()
        {
            InitializeComponent();

            BindingContext = new RootPageFlyoutViewModel();
            ListView = MenuItemsListView;
        }

        class RootPageFlyoutViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<RootPageFlyoutMenuItem> MenuItems { get; set; }

            public RootPageFlyoutViewModel()
            {
                MenuItems = new ObservableCollection<RootPageFlyoutMenuItem>(new[]
                {
                    new RootPageFlyoutMenuItem { Id = 0, Title = "Home", TargetType = typeof(RootPage) },
                    new RootPageFlyoutMenuItem { Id = 1, Title = "Forecast", TargetType = typeof(WeatherTabbedPage) },
                    new RootPageFlyoutMenuItem { Id = 2, Title = "Music", TargetType = typeof(MusicPage) },
                    new RootPageFlyoutMenuItem { Id = 3, Title = "User", TargetType = typeof(UserTabbedPage) },
                    new RootPageFlyoutMenuItem { Id = 4, Title = "About", TargetType = typeof(AboutPage) },
                });
            }

            #region INotifyPropertyChanged Implementation
            public event PropertyChangedEventHandler PropertyChanged;
            void OnPropertyChanged([CallerMemberName] string propertyName = "")
            {
                if (PropertyChanged == null)
                    return;

                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            #endregion
        }
    }
}