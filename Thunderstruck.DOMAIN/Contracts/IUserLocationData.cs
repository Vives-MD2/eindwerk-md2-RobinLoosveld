using System.Threading.Tasks;
using Thunderstruck.DOMAIN.Models;

namespace Thunderstruck.DOMAIN.Contracts
{
    public interface IUserLocationData:IBase<LocationData>
    {
        Task<UserAchievement> GetByIdAsync(int userId, int locationDataId);
    }
}