using Thunderstruck.DOMAIN.Helpers;

namespace Thunderstruck.DOMAIN.Models
{
    public class UserLocationData : GObject
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public int LocationDataId { get; set; }
        public LocationData LocationData { get; set; }
    }
}