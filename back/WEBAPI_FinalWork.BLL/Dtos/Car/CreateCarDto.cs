using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WEBAPI_FinalWork.DAL.Entities;

namespace WEBAPI_FinalWork.BLL.Dtos.Car
{
    public class CreateCarDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public int Year { get; set; } = 0;
        [Required]
        public float Volume { get; set; } = 0f;
        [Required]
        public int Price { get; set; } = 0;
        public string? Description { get; set; }
        public IFormFile? Image { get; set; }
        [Required]
        public int ManufacturerId { get; set; }
    }
}
