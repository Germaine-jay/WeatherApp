using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Reflection.Metadata;
using WeatherApp.BLL.Interface;
using WeatherApp.BLL.Models;
using WeatherApp.DAL.Entities;
using WeatherApp.DAL.Repository;

namespace WeatherApp.BLL.Implementation
{
    public class WeatherServices : IWeatherServices
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<City> _cityRepo;
        private readonly HttpClient _httpClient;
        private readonly string? _ApiKey;
        private readonly IConfiguration _configuration;
   

        public WeatherServices(IConfiguration configuration, IUnitOfWork unitOfWork)
        {

            _unitOfWork = unitOfWork;
            _cityRepo = _unitOfWork.GetRepository<City>();
            _configuration = configuration;
            _httpClient = new HttpClient();
            _ApiKey = _configuration?.GetSection("Weather").GetSection("ApiKey").Value;
        }
        public async Task<IEnumerable<WeatherVM>> GetWeather()
        {
            var cityList = await _cityRepo.GetAllAsync();
            var tasks = new List<Task<WeatherVM>>();

            using (var httpClient = _httpClient)
            {
                foreach (var city in cityList)
                {
                    var apiUrl = $"https://api.openweathermap.org/data/2.5/weather?q={city.CityName}&units=metric&appid={_ApiKey}";

                    tasks.Add(FetchWeatherAsync(httpClient, apiUrl));
                   
                }

                await Task.WhenAll(tasks);

                return tasks.Select(t => t.Result);
            }
        }

        private async Task<WeatherVM> FetchWeatherAsync(HttpClient httpClient, string apiUrl)
        {
            var response = await httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<WeatherResponse>(json);

                if (data.Cod != "404")
                {
                    var iconurl = "http://openweathermap.org/img/w/" + data.Weather[0].Icon + ".png";

                    var weatherViewModel = new WeatherVM
                    {
                        Name = data.Name,
                        Visibility = data.Visibility,
                        WeatherDescription = data.Weather[0].Description,
                        WeatherIcon = iconurl,
                        WindSpeed = data.Wind.Speed,
                        MainHumidity = data.Main.Humidity,
                        MainTemprature = data.Main.Temp
                    };
                    return weatherViewModel;
                }

            }

            return null;
        }
        
    }
}

