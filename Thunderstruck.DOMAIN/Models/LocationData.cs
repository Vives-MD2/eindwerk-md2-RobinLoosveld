using System;
using System.Collections.Generic;
using NetTopologySuite.Geometries;
using Thunderstruck.DOMAIN.Helpers;

namespace Thunderstruck.DOMAIN.Models
{
    public class LocationData : GObject
    {
        public int Id { get; set; }
        public string LocationName { get; set; }
        public DateTime TimeStamp { get; set; }
        public Point Location { get; set; }

        public ICollection<UserLocationData> UserLocationsData { get; set; }
    }
}