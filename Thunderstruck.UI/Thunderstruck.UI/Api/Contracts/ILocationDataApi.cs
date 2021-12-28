using System.Threading.Tasks;
using Refit;
using Thunderstruck.DOMAIN.Models;

namespace Thunderstruck.UI.Api.Contracts
{
    public interface ILocationDataApi
    {
        [Get("locationdata/getbyid?id={id}")]
        Task<string> GetById(int id);
        [Get("/locationdata/get?skip&take={take}")]
        Task<string> Get(int skip, int take);
        [Post("/locationdata/create")]
        Task<string> Create([Body] LocationData locationData);
        [Put("locationdata/update")]
        Task<string> Update([Body] LocationData locationData);
        [Delete("locationdata/delete")]
        Task<string> Delete([Body] LocationData locationData);
    }
}