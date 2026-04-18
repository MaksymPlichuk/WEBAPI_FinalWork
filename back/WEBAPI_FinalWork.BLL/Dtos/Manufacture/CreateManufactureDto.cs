using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WEBAPI_FinalWork.DAL.Entities;

namespace WEBAPI_FinalWork.BLL.Dtos.Manufacture
{
    public class CreateManufactureDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        [Required]
        public int NumberOfWorkers { get; set; } = 0;
        [Required]
        public DateTime FoundedDate { get; set; } = DateTime.UtcNow;
        [Required]
        public int CountryId { get; set; }
    }
}
