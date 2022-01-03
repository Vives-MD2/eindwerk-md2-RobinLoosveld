using Thunderstruck.DOMAIN.Models;
using Thunderstruck.UI.Views.Project.Music;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Thunderstruck.UI.Views.Project.Music
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MusicPage : TabbedPage
    {
        public MusicPage(User user)
        {
            InitializeComponent();
        }
    }
}