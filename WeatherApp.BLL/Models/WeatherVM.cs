using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.BLL.Implementation;

namespace WeatherApp.BLL.Models
{
    public class WeatherVM
    {
        public string Visibility { get; set; } 
        public string Name { get; set; }
        
        public string? WeatherMain { get; set; }
        public string? WeatherDescription { get; set; }
        public string? WeatherIcon { get; set; }
          
        public int WindSpeed { get; set; }
        public int WindDeg { get; set; }
        
       
        public string? MainTemprature { get; set; }
        public string? MainFeelsLike { get; set; }
        public string? MainTempMin { get; set; }
        public string? MainTempMax { get; set; }
        public string MainPressure { get; set; }
        public string MainHumidity { get; set; }

        /*public IEnumerable<WeatherResponse.Main> Main { get; set; }
        public IEnumerable<WeatherResponse.Weather> Weather { get; set; }
        public IEnumerable<WeatherResponse.Wind> Wind { get; set; }*/
    }
}
