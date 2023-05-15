using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.BLL.Models
{
    public class WeatherResponse
    {
        public string visibility { get; set; }
        public string name { get; set; }
        public Weather[] weather { get; set; }
        public Wind wind { get; set; }
        public Main main { get; set; }

        public class Weather
        {
            public int id { get; set; }
            public string main { get; set; }
            public string description { get; set; }
            public string icon { get; set; }
        }

        public class Wind
        {
            public int speed { get; set; }
            public int deg { get; set; }
            public int id { get; set; }
        }

        public class Main
        {
            public string temp { get; set; }
            public string feels_like { get; set; }
            public string temp_min { get; set; }
            public string temp_max { get; set; }
            public string pressure { get; set; }
            public string humidity { get; set; }
        }
    }
