using WeatherApp.BLL.Models;

namespace WeatherApp.BLL.Interface
{
    public interface IWeatherServices
    {
        Task<WeatherResponse> GetWeather();
    }
}
