
using System;
using System.Linq;
using System.Threading.Tasks;
using NetTopologySuite.Geometries;
using Xamarin.Essentials;
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
        /// <summary>
        /// Get a placemark by coordinates Example: Coordinates could have been provided by the phone location, but an address is needed.
        /// </summary>
        /// <param name="lat">double latitude</param>
        /// <param name="lon">double longitude</param>
        /// <returns>The correct placemark</returns>
        public static async Task<Placemark> GetPlacemarkByCoordinates(double lat, double lon)
        {
            if (!double.IsNaN(lat) && !double.IsNaN(lon))
            {
                var placemarks = await Geocoding.GetPlacemarksAsync(lat, lon);
                var placemark = placemarks?.FirstOrDefault();
                return placemark;
            }
            return null;
        }
    }
}