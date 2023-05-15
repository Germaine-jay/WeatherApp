using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.BLL.Interface;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using WeatherApp.DAL.Entities;
using WeatherApp.DAL.Repository;

namespace WeatherApp.BLL.Implementation
{
    public class WeatherServices: IWeatherServices
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<City> _cityRepo;
        private readonly HttpClient _httpClient;
        private readonly string? _ApiKey;
        private readonly IConfiguration _configuration;

        public WeatherServices(IConfiguration configuration, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _cityRepo = _unitOfWork.GetRepository<City>();
            _configuration = configuration;
            _httpClient = new HttpClient();
            _ApiKey = _configuration?.GetSection("Weather").GetSection("ApiKey").Value;
        }
        public async Task<WeatherResponse> GetWeather()
        {
            foreach (var city in await _cityRepo.GetAllAsync())
            {
                _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _ApiKey);
                var apiUrl = $"https://api.openweathermap.org/data/2.5/weather?q={city}&units={_ApiKey}";

                var recipientResponse = await _httpClient.GetAsync(apiUrl);

                if (recipientResponse.IsSuccessStatusCode && string.IsNullOrEmpty(apiUrl))
                {
                    string listResponse = await recipientResponse.Content.ReadAsStringAsync();
                    WeatherResponse getResponse = JsonConvert.DeserializeObject<WeatherResponse>(listResponse);

                    return getResponse;
                }
            }
            return null;
        }
    }

    public class WeatherResponse
    {
        public string visibility { get; set; }
        public string name { get; set; }
        public Weather[] weather { get; set; }
        public Wind wind { get; set; }
        public Main main { get; set; }

        public class Weather
        {
            public int id { get; set; }
            public string main { get; set; }
            public string description { get; set; }
            public string icon { get; set; }
        }

        public class Wind
        {
            public int speed { get; set; }
            public int deg { get; set; }
            public int id { get; set; }
        }

        public class Main
        {
            public string temp { get; set; }
            public string feels_like { get; set; }
            public string temp_min { get; set; }
            public string temp_max { get; set; }
            public string pressure { get; set; }
            public string humidity { get; set; }
        }
    }
}

