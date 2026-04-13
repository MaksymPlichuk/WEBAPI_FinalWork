using Microsoft.AspNetCore.Mvc;
using WEBAPI_FinalWork.DAL.Repositories;

namespace WEBAPI_FinalWork.API.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class ManufactureController : ControllerBase
    {
        private readonly ManufactureRepository _manufactureRepository;
        public ManufactureController(ManufactureRepository manufactureRepository)
        {
            _manufactureRepository = manufactureRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetManufactures()
        {
            throw new NotImplementedException();
        }
    }
}
