using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WEBAPI_FinalWork.DAL.Entities
{
    public class CarEntity : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public int Year { get; set; } = 0;
        public float Volume { get; set; } = 0f;
        public int Price { get; set; } = 0;
        public string? Description { get; set; }
        public string? Image { get; set; }
        public int ManufacturerId { get; set; }
        public ManufacturerEntity? Manufacturer { get; set; }
    }
}
