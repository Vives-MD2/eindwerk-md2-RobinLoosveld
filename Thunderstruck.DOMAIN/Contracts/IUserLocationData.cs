using System.Threading.Tasks;
using Thunderstruck.DOMAIN.Models;

namespace Thunderstruck.DOMAIN.Contracts
{
    public interface IUserLocationData:IBase<UserLocationData>
    {
        Task<UserLocationData> GetByIdAsync(int userId, int locationDataId);
    }
}