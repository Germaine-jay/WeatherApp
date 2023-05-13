using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WeatherApp.BLL.Interface;
using WeatherApp.BLL.Models;

namespace WeatherApp.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class WeatherController : Controller
    {
        private readonly ICitiesServices _citiesService;

        public WeatherController(ICitiesServices citiesService)
        {
            _citiesService = citiesService;
        }

        /*public async Task<IActionResult> Index(int Id)
        {
            var city = await _citiesService.GetCity(Id);
            return View(city);
        }*/

        public async Task<IActionResult> Cities()
        {
            var model = await _citiesService.GetCities();
            return View(model);
        }

        public async Task<IActionResult> HomePage(int Id)
        {
            var city = await _citiesService.GetCity(Id);
            return View(city);
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
        public async Task<IActionResult> Save(AddOrUpdateVM model)
        {
            if (ModelState.IsValid)
            {

                var (successful, msg) = await _citiesService.AddOrUpdateAsync(model);

                if (successful)
                {

                    TempData["SuccessMsg"] = msg;

                    return RedirectToAction("Index");
                }

                TempData["ErrMsg"] = msg;
                return View("Index");

            }
            return View("Index");
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
                    return RedirectToAction("Index");
                }

                TempData["ErrMsg"] = msg;
                return View("Index");
            }
            return View("Index");
        }

        [HttpPost("{Id}")]
        public async Task<IActionResult> Delete(int userId)
        {
            if (ModelState.IsValid)
            {

                var (success, msg) = await _citiesService.DeleteAsync(userId);
                if (success)
                {
                    TempData["SuccessMsg"] = msg;
                    return RedirectToAction("Index");
                }

                TempData["ErrMsg"] = msg;
                return RedirectToAction("Index");

            }
            return View("Index");

        }

        [HttpPost]
        public async Task<IActionResult> SaveAddOrUpdate(AddOrUpdateVM model)
        {
            if (ModelState.IsValid)
            {

                var (successful, msg) = await _citiesService.AddOrUpdateAsync(model);

                if (successful)
                {

                    TempData["SuccessMsg"] = msg;
                    return RedirectToAction("Index");
                }

                TempData["ErrMsg"] = msg;
                return View("Index");

            }
            return View("Index");
        }
    }
}
