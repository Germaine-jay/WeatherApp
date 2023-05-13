using AutoMapper;
using WeatherApp.BLL.Models;
using WeatherApp.DAL.Entities;

namespace WeatherApp.BLL.MappingProfiles
{
    public class CityMappingProfile : Profile
    {
        public CityMappingProfile()
        {
            CreateMap<City, CityVM>();
            CreateMap<CityVM, City>();

        }
    }
}
