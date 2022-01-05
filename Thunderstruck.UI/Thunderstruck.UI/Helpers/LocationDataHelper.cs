
using System;
using NetTopologySuite.Geometries;
using Location = Xamarin.Essentials.Location;


namespace Thunderstruck.UI.Helpers
{
    public static class LocationDataHelper
    {
        /// <summary>
        /// De datatype of the Geolocation return object is Location. The Datatype in the database is a NetTopology Point.
        /// This method will convert a Location to a Point.
        /// </summary>
        /// <param name="location">Geolocation return type</param>
        /// <returns>Point point</returns>
        public static Point ConvertLocationToPoint(Location location)
        {
            Point point = new Point(location.Longitude, location.Latitude);
            return point;
        }
    }
}