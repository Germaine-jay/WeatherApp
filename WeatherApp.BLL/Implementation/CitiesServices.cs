using AutoMapper;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using WeatherApp.BLL.Interface;
using WeatherApp.BLL.Models;
using WeatherApp.DAL.Entities;
using WeatherApp.DAL.Repository;

namespace WeatherApp.BLL.Implementation
{
    public class CitiesServices : ICitiesServices
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<City> _cityRepo;
        private readonly IConfiguration _configuration;

        public CitiesServices(IConfiguration configuration, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _cityRepo = _unitOfWork.GetRepository<City>();
            _configuration = configuration;
        }

        public async Task<(bool successful, string msg)> Create(CityVM model)
        {
            var user = _mapper.Map<City>(model);
            var rowChanges = await _cityRepo.AddAsync(user);

            return rowChanges != null ? (true, $"User: {model.CityName} was successfully created!") : (false, "Failed To create user!");
        }


        public async Task<IEnumerable<CityVM>> GetCities()
        {
            var cities = await _cityRepo.GetAllAsync();
            var citiesViewModels = cities.Select(u => new CityVM
            {
                Id = u.Id,
                CityName = u.CityName,
            });

            return citiesViewModels;
        }
        public async Task<CityVM> GetCity(int Id)
        {
            var user = await _cityRepo.GetSingleByAsync(u => u.Id == Id);
            var Auser = _mapper.Map<CityVM>(user);

            return Auser;
        }


        public async Task<(bool successful, string msg)> Update(CityVM model)
        {

            var city = await _cityRepo.GetSingleByAsync(u => u.Id == model.Id);
            if (city == null)
            {
                return (false, $"City with Name:{model.CityName} wasn't found");
            }

            var userupdate = _mapper.Map(model, city);
            var rowChanges = await _cityRepo.UpdateAsync(userupdate);

            return rowChanges != null ? (true, $"City Name update was successful!") : (false, "Failed To save changes!");

        }


        public async Task<(bool successful, string msg)> DeleteAsync(int Id)
        {
            var city = await _cityRepo.GetSingleByAsync(u => u.Id == Id);

            if (city == null)
            {
                return (false, $"User with user:{city?.Id} wasn't found");
            }

            await _cityRepo.DeleteAsync(city);
            return await _unitOfWork.SaveChangesAsync() >= 0 ? (true, $"{city.CityName} was deleted") : (false, $"Delete Failed");
        }


        public async Task<(bool successful, string msg)> AddOrUpdateAsync(CityVM model)
        {
            var cities = await _cityRepo.GetAllAsync();
            City cityId = await _cityRepo.GetSingleByAsync(u => u.Id == model.Id);

            var inputCity = cities.Any(c => c.CityName == model.CityName);

            if (cityId == null || String.IsNullOrEmpty($"{cityId.Id}"))
            {
                var city = _mapper.Map<City>(model);
                if(inputCity == false)
                {

                    var addCity = await _cityRepo.AddAsync(city);
                    return addCity != null ? (true, $"City: {model.CityName} was successfully created!") : (false, "Failed To create user!");
                }
                return (false, "Entry Already Exist");

            }


            var userupdate = _mapper.Map(model, cityId);
            var rowChanges = await _cityRepo.UpdateAsync(userupdate);

            return rowChanges != null ? (true, $"City Name update was successful!") : (false, "Failed To save changes!");

        }

       /* public async Task<(bool successful, string msg)> ValidateInput(string place)
        {
            var apiUrl = "https://api.openweathermap.org/data/2.5/weather?q=Helsinki&units=metric&appid=YOUR_API_KEY";
            // Replace "YOUR_API_KEY" with your actual API key from OpenWeatherMap

            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<dynamic>(json);



                    return weatherViewModels;
                }
            }
        }*/
    }
}
