using System;
using System.Collections.Generic;
using Thunderstruck.DOMAIN.Helpers;

namespace Thunderstruck.DOMAIN.Models
{
    public abstract class Achievement : GObject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public String Description { get; set; }
        public int Reward { get; set; }
        public string IconImageLink { get; set; }

        public ICollection<UserAchievement> UserAchievements { get; set; }
        //public AchievementFirstRain FirstRain { get; set; }

        //public AchievementHighVoltage AchievementHighVoltage { get; set; }
        //public AchievementListening AchievementListening { get; set; }
        //public AchievementSpeed AchievementSpeed { get; set; }
    }
}