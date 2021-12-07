using Thunderstruck.DOMAIN.Helpers;

namespace Thunderstruck.DOMAIN.Models
{
    public class UserAchievement : GObject
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public int AchievementId { get; set; }
        public Achievement Achievement { get; set; }
    }
}