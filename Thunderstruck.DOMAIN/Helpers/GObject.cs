using System;

namespace Thunderstruck.DOMAIN.Helpers
{

    public class GObject
    {
        private bool success = true;

        public bool Successful
        {
            get { return success; }
        }

        private ThunderstruckException tex = null;

        public ThunderstruckException Tex
        {
            get { return tex; }
            set
            {
                success = false;
                tex = value;
            }
        }

    }

}