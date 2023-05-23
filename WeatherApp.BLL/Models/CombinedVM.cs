using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.BLL.Models
{
    public class CombinedVM
    {
        public WeatherApp.BLL.Models.AddOrUpdateVM CityVM { get; set; }
        public IEnumerable<WeatherApp.BLL.Models.WeatherVM> WeatherVM { get; set; }

    }
}
