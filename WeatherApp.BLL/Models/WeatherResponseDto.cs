using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.BLL.Models
{
    public class WeatherResponse
    {
        public string Name { get; set; }
        public int Visibility { get; set; }
        public string Cod { get; set; }
        public string Message { get; set; }
        public WeatherInfo[] Weather { get; set; }
        public WindInfo Wind { get; set; }
        public MainInfo Main { get; set; }
    }

    public class WeatherInfo
    {
        public string Description { get; set; }
        public string Icon { get; set; }
    }

    public class WindInfo
    {
        public double Speed { get; set; }
    }

    public class MainInfo
    {
        public int Humidity { get; set; }
        public double Temp { get; set; }
    }
}

