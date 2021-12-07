using Thunderstruck.DOMAIN.Helpers;

namespace Thunderstruck.DOMAIN.Models
{
    public class AchievementListening : GObject
    {
        public int Id { get; set; }
        public int AchievementId { get; set; }
        public Achievement Achievement { get; set; }
        public double TotalPlayTime { get; set; }
    }
}