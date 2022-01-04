using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Thunderstruck.DOMAIN.Models;
using Thunderstruck.UI.ResponseModels.UserModels;

namespace Thunderstruck.UI.Helpers
{
    public static class UserHelper
    {
        /// <summary>
        /// Before you can add a spotify user to the database, you need to map the spotifyuserobject to a regular (database) user object.
        /// You provide the spotifyuserobject and the method returns the new db user.
        /// </summary>
        /// <param name="spotifyUser">The SpotifyUser response model</param>
        /// <returns>Returns a remapped User object</returns>
        public static async Task<User> MapSpotifyUserToDbUser(SpotifyUserRootModel spotifyUser)
        {
            var dbUser = new User()
            {
                Username = spotifyUser.display_name,
                Email = spotifyUser.email
            };
            return dbUser;
        }

        /// <summary>
        /// The access token expires after 1 hour. Check if the current token needs to be refreshed
        /// </summary>
        /// <param name="expirationDateTimestamp"></param>
        /// <returns>bool = isExpired</returns>
        public static async Task<bool> CheckIfTokenHasExpired(DateTimeOffset expirationDateTimestamp, string authToken)
        {
            bool isExpired = false;
            if (expirationDateTimestamp.CompareTo(DateTimeOffset.Now) > 0)
            {
                //is equal to current time or has expired
                isExpired = true;
            }
            return isExpired;
        }

        /// <summary>
        /// Refresh the expired access_token and return a new one. You need to provide the refresh_token.
        /// </summary>
        /// <param name="refreshToken"></param>
        /// <returns>string: new acces</returns>
        public static async Task<string> RefreshToken(string refreshToken)
        {
            //todo: needs to be implemented
            string newAccessToken = string.Empty;
            return newAccessToken;
        }
    }
}
