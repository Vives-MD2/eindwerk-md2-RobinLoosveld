using System.Threading.Tasks;
using Xamarin.Forms;

namespace Thunderstruck.UI.AppService.Contracts
{
    public interface IPageService
    {
        Task DisplayAlert(string title, string message, string cancel);
        Task<bool> DisplayAlert(string title, string message, string accept, string cancel);
        Task PushAsync(Page page);
        Task PushModelAsync(Page page);
    }
}