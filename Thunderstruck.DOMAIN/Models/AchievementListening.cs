using System;
using Thunderstruck.DOMAIN.Helpers;

namespace Thunderstruck.DOMAIN.Models
{
    public class AchievementListening : Achievement
    {
        public DateTime TotalPlayTime { get; set; }
    }
}