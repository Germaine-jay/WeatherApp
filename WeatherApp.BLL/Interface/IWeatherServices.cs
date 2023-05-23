using WeatherApp.BLL.Models;
using static WeatherApp.BLL.Implementation.WeatherServices;

namespace WeatherApp.BLL.Interface
{
    public interface IWeatherServices
    {
        Task<IEnumerable<WeatherVM>> GetWeather();
    }
}
