using System.Collections.Generic;
using Thunderstruck.DOMAIN.Helpers;

namespace Thunderstruck.DOMAIN.Models
{

    public class AchievementFirstRain : Achievement
    {
        //public Achievement Achievement { get; set; }
        public bool IsRaining { get; set; }
    }
}