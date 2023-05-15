using WeatherApp.BLL.Implementation;
using WeatherApp.BLL.Models;

namespace WeatherApp.BLL.Interface
{
    public interface ICitiesServices
    {
        Task<(bool successful, string msg)> Create(CityVM model);
        Task<(bool successful, string msg)> DeleteAsync(int Id);
        Task<(bool successful, string msg)> Update(CityVM model);
        Task<IEnumerable<CityVM>> GetCities();
        Task<CityVM> GetCity(int Id);
        Task<(bool successful, string msg)> AddOrUpdateAsync(CityVM model);
        
    }
}
