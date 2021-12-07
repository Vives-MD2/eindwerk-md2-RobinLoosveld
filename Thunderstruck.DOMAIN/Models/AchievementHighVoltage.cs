using System.Collections.Generic;
using Thunderstruck.DOMAIN.Helpers;

namespace Thunderstruck.DOMAIN.Models
{
    public class AchievementHighVoltage : GObject
    {
        public int Id { get; set; }
        public int AchievementId { get; set; }
        public Achievement Achievement { get; set; }
        public bool IsThunderstorm { get; set; }
    }
}