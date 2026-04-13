using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WEBAPI_FinalWork.DAL.Entities;

namespace WEBAPI_FinalWork.BLL.Dtos.Car
{
    public class CarDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Year { get; set; } = 0;
        public float Volume { get; set; } = 0f;
        public int Price { get; set; } = 0;
        public string? Description { get; set; }
        public string? Image { get; set; }
        public ManufacturerEntity? Manufacturer { get; set; }
    }
}
