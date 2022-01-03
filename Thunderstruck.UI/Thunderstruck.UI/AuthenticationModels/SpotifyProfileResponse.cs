using System.Collections.Generic;

namespace Thunderstruck.UI.AuthenticationModels
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class ExplicitContent
    {
        public bool filter_enabled;
        public bool filter_locked;
    }

    public class ExternalUrls
    {
        public string spotify;
    }

    public class Followers
    {
        public object href;
        public int total;
    }

    public class SpotifyUserRoot
    {
        public string country;
        public string display_name;
        public string email;
        public ExplicitContent explicit_content;
        public ExternalUrls external_urls;
        public Followers followers;
        public string href;
        public string id;
        public List<object> images;
        public string product;
        public string type;
        public string uri;
    }
}