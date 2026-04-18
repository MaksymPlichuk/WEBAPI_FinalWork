using Microsoft.AspNetCore.Mvc;
using WEBAPI_FinalWork.API.Extensions;
using WEBAPI_FinalWork.API.Settings;
using WEBAPI_FinalWork.BLL.Dtos.Car;
using WEBAPI_FinalWork.BLL.Dtos.Pagination;
using WEBAPI_FinalWork.BLL.Services;

namespace WEBAPI_FinalWork.API.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class CarController: ControllerBase
    {
        private readonly CarService _carService;
        private readonly string _storagePath;
        public CarController(CarService carService, IWebHostEnvironment env)
        {
            _carService = carService;
            string root = env.ContentRootPath;
            _storagePath = Path.Combine(root, StaticFilesSettings.Storage, StaticFilesSettings.CarDir);
        }
        [HttpGet]
        public async Task<IActionResult> GetCars([FromQuery]PaginationDto pagination)
        {
            var res = await _carService.GetAllAsync(pagination);
            return this.GetAction(res);
        }

        [HttpGet("by-id")]
        public async Task<IActionResult> GetById([FromQuery]int id)
        {
            var res = await _carService.GetByIdAsync(id);
            return this.GetAction(res);
        }
        [HttpGet("by-name")]
        public async Task<IActionResult> GetByName([FromQuery]string name)
        {
            var res = await _carService.GetByNameAsync(name);
            return this.GetAction(res);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCar([FromForm] CreateCarDto dto)
        {
            var res = await _carService.CreateCarAsync(dto, _storagePath);
            return this.GetAction(res);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateCar([FromForm] UpdateCarDto dto)
        {
            var res = await _carService.UpdateCarAsync(dto, _storagePath);
            return this.GetAction(res);
        }

        [HttpDelete("by-id")]
        public async Task<IActionResult> DeleteById([FromQuery] int id)
        {
            var res = await _carService.DeleteByIdAsync(id, _storagePath);
            return this.GetAction(res);
        }
        [HttpDelete("by-name")]
        public async Task<IActionResult> DeleteByName([FromQuery] string name)
        {
            var res = await _carService.DeleteByName(name, _storagePath);
            return this.GetAction(res);
        }
    }
}
