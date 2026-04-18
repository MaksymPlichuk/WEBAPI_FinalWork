using Microsoft.AspNetCore.Mvc;
using WEBAPI_FinalWork.API.Extensions;
using WEBAPI_FinalWork.BLL.Dtos.Manufacture;
using WEBAPI_FinalWork.BLL.Dtos.Pagination;
using WEBAPI_FinalWork.BLL.Services;
using WEBAPI_FinalWork.DAL.Repositories;

namespace WEBAPI_FinalWork.API.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class ManufactureController : ControllerBase
    {
        private readonly ManufactureService _manufactureService;
        public ManufactureController(ManufactureService manufactureService)
        {
            _manufactureService = manufactureService;
        }

        [HttpGet]
        public async Task<IActionResult> GetManufactures([FromQuery]PaginationDto pagination)
        {
            var res = await _manufactureService.GetAllAsync(pagination);
            return this.GetAction(res);
        }
        [HttpGet("by-id")]
        public async Task<IActionResult> GetManuByIdAsync([FromQuery]int id)
        {
            var res = await _manufactureService.GetByIdAsync(id);
            return this.GetAction(res);
        }
        [HttpGet("by-name")]
        public async Task<IActionResult> GetManuByNameAsync([FromQuery]string name)
        {
            var res = await _manufactureService.GetByNameAsync(name);
            return this.GetAction(res);
        }

        [HttpPost]
        public async Task<IActionResult> CreateManufacturerAsync([FromForm]CreateManufactureDto dto)
        {
            var res = await _manufactureService.CreateManufacturerAsync(dto);
            return this.GetAction(res);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateManufacturerAsync([FromForm]UpdateManufactureDto dto)
        {
            var res = await _manufactureService.UpdateManufacturerAsync(dto);
            return this.GetAction(res);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteManufacturerByIdAsync(int id)
        {
            var res = await _manufactureService.DeleteByIdAsync(id);
            return this.GetAction(res);
        }
        [HttpDelete("by-name")]
        public async Task<IActionResult> DeleteManufacturerByNameAsync(string name)
        {
            var res = await _manufactureService.DeleteByName(name);
            return this.GetAction(res);
        }
    }
}
