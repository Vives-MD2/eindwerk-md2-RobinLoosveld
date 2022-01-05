using System;
using NetTopologySuite.Geometries;

namespace Thunderstruck.UI.ResponseModels.WeatherModels
{
    public class LocationDataWithDouble
    {
        public string? LocationName { get; set; }
        public DateTime TimeStamp { get; set; }
        
        //To later convert to Location
        //public Point Location { get; set; }
        public Double XLongitude { get; set; }
        public Double YLatitude { get; set; }
    }
}