namespace WeatherApp.BLL.Models
{

    public class WeatherVM
    {
        public string Name { get; set; }
        public int Visibility { get; set; }
        public string WeatherDescription { get; set; }
        public string WeatherIcon { get; set; }
        public double WindSpeed { get; set; }
        public int MainHumidity { get; set; }
        public double MainTemprature { get; set; }
    }
}
