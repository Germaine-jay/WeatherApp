using Microsoft.AspNetCore.Mvc;
using WeatherApp.BLL.Interface;
using WeatherApp.BLL.Models;

namespace WeatherApp.Controllers
{
    [Route("[controller]/[action]/{id?}")]
    [AutoValidateAntiforgeryToken]
    public class WeatherController : Controller
    {
        private readonly ICitiesServices _citiesService;
        private readonly IWeatherServices _weatherServices;

        public WeatherController(ICitiesServices citiesService, IWeatherServices weatherServices)
        {
            _citiesService = citiesService;
            _weatherServices = weatherServices;
        }


        public async Task<IActionResult> HomePage(int Id)
        {
            var city = await _citiesService.GetCity(Id);
            return View(city);
        }


        public async Task<IActionResult> AllCitiesWeather()
        {
            var cities = await _weatherServices.GetWeather();      
            return View(cities);
        }

        public async Task<IActionResult> Cities()
        {
            var model = await _citiesService.GetCities();
            return View(model);
        }



        public async Task<IActionResult> City(int Id)
        {
            var city = await _citiesService.GetCity(Id);
            return View(city);

        }


        public IActionResult DeleteCity(int Id)
        {
            return View(new CityVM { Id = Id });

        }


        [HttpPost]
        public async Task<IActionResult> Save(CityVM model)
        {
            if (ModelState.IsValid)
            {
                var (successful, msg) = await _citiesService.AddOrUpdateAsync(model);

                if (successful)
                {

                    TempData["SuccessMsg"] = msg;

                    return RedirectToAction("HomePage");
                }

                TempData["ErrMsg"] = msg;
                return View("HomePage");

            }
            return View("HomePage");
        }

        [HttpPut]
        public async Task<IActionResult> SaveUpdate(CityVM model)
        {

            if (ModelState.IsValid)
            {
                var (successful, msg) = await _citiesService.Update(model);

                if (successful)
                {
                    TempData["SuccessMsg"] = msg;
                    return RedirectToAction("HomePage");
                }

                TempData["ErrMsg"] = msg;
                return View("HomePage");
            }
            return View("HomePage");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int Id)
        {
            if (ModelState.IsValid)
            {

                var (success, msg) = await _citiesService.DeleteAsync(Id);
                if (success)
                {
                    TempData["SuccessMsg"] = msg;
                    return RedirectToAction("Cities");
                }

                TempData["ErrMsg"] = msg;
                return RedirectToAction("Cities");

            }
            return View("Cities");

        }

        [HttpPost]
        public async Task<IActionResult> SaveAddOrUpdate(CityVM model)
        {
            if (ModelState.IsValid)
            {

                var (successful, msg) = await _citiesService.AddOrUpdateAsync(model);

                if (successful)
                {

                    TempData["SuccessMsg"] = msg;
                    return RedirectToAction("HomePage");
                }

                TempData["ErrMsg"] = msg;
                return View("HomePage");

            }
            return View("HomePage");
        }
    }
}
