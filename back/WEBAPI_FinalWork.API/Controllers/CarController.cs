using Microsoft.AspNetCore.Mvc;
using WEBAPI_FinalWork.API.Extensions;
using WEBAPI_FinalWork.BLL.Dtos.Car;
using WEBAPI_FinalWork.BLL.Services;

namespace WEBAPI_FinalWork.API.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class CarController: ControllerBase
    {
        private readonly CarService _carService;
        public CarController(CarService carService)
        {
            _carService = carService;
        }
        [HttpGet]
        public async Task<IActionResult> GetCars()
        {
            var res = await _carService.GetAllAsync();
            return this.GetAction(res);
        }
    }
}
