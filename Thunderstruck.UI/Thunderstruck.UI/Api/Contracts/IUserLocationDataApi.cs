using System.Threading.Tasks;
using Refit;
using Thunderstruck.DOMAIN.Models;

namespace Thunderstruck.UI.Api.Contracts
{
    public interface IUserLocationDataApi
    {
        [Get("/userlocationdata/getbyid?id={id}=locationDataId={locationDataId}")]
        Task<string> GetById(int userId, int locationDataId);
        [Get("/userlocationdata/get?skip={skip}&take={take}")]
        Task<string> Get(int skip, int take);
        [Post("/userlocationdata/create")]
        Task<string> Create([Body] UserLocationData userLocationData);
        [Put("userlocationdata/update")]
        Task<string> Update([Body] UserLocationData userLocationData);
        [Delete("userlocationdata/delete")]
        Task<string> Delete([Body] UserLocationData userLocationData);
    }
}