using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.BLL.Models
{
    public class PlaceData
    {
        public string Error { get; set; }
        public string Msg { get; set; }
        public Data[] Data { get; set; }
    }

    public class Data
    {
        public string Name { get; set; }
        public States[] States { get; set; }
    }

    public class States
    {
        public string Name { get; set; }
    }
}
