using System.Collections.Generic;
using Thunderstruck.DOMAIN.Helpers;

namespace Thunderstruck.DOMAIN.Models
{
    public class User : GObject
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public ICollection<UserAchievement> UserAchievements { get; set; }
        public ICollection<UserLocationData> UserLocationsData { get; set; }
    }
}