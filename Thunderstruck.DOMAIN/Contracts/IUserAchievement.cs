using System.Threading.Tasks;
using Thunderstruck.DOMAIN.Models;

namespace Thunderstruck.DOMAIN.Contracts
{
    public interface IUserAchievement:IBase<UserAchievement>
    {
        Task<UserAchievement> GetByIdAsync(int userId, int achievementId);
    }
}