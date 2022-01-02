using System.Collections.Generic;

namespace Thunderstruck.UI.AuthenticationModels.CurrentWeatherModel
{
    public class CurrentWeatherModel
    {
        //Todo 1 klasse van maken
        public class Coord
        {
            public double lon;
            public double lat;
        }

        public class Weather
        {
            public int id;
            public string main;
            public string description;
            public string icon;
        }

        public class Main
        {
            public double temp;
            public double feelslike;
            public double tempmin;
            public double temp_max;
            public int pressure;
            public int humidity;
        }
        public class Wind
        {
            public double speed;
            public int deg;
            public int gust;
        }

        public class Clouds
        {
            public int all;
        }
        public class Sys
        {
            public int type;
            public int id;
            public string country;
            public int sunrise;
            public int sunset;
        }

        public class CurrentWeatherRootModel
        {
            public Coord coord;
            public List<Weather> weather;
            public string @base;
            public Main main;
            public int visibility;
            public Wind wind;
            public Clouds clouds;
            public int dt;
            public Sys sys;
            public int timezone;
            public int id;
            public string name;
            public int cod;
        }


    }

    //public class CurrentWeatherModel
    //{
    //    //Todo 1 klasse van maken

    //    public double lon;
    //    public double lat;


    //    public int id;
    //    public string main;
    //    public string description;
    //    public string icon;


    //    public double temp;
    //    public double feelslike;
    //    public double tempmin;
    //    public double temp_max;
    //    public int pressure;
    //    public int humidity;


    //    public double speed;
    //    public int deg;
    //    public int gust;



    //    public int all;



    //    public int type;
    //    public int id;
    //    public string country;
    //    public int sunrise;
    //    public int sunset;



    //    public Coord coord;
    //    public List<Weather> weather;
    //    public string @base;
    //    public Main main;
    //    public int visibility;
    //    public Wind wind;
    //    public Clouds clouds;
    //    public int dt;
    //    public Sys sys;
    //    public int timezone;
    //    public int id;
    //    public string name;
    //    public int cod;


    //}
}