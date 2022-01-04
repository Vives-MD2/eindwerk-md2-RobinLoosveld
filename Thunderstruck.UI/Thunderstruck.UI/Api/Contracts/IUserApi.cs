using System.Threading.Tasks;
using Refit;
using Thunderstruck.DOMAIN.Models;

namespace Thunderstruck.UI.Api.Contracts
{
    public interface IUserApi
    {
        [Get("/user/getbyid?id={id}")]
        Task<string> GetById(int id);

        [Get("/user/getbyemail?email={email}")]
        Task<string> GetByEmail(string email);

        [Get("/student/get?skip={skip}&take={take}")]
        Task<string> Get(int skip, int take);

        [Post("/user/create")]
        Task<string> Create([Body] User user);

        [Put("/user/update")]
        Task<string> Update([Body] User user);

        [Delete("/user/delete")]
        Task<string> Delete([Body] User user);
    }
}