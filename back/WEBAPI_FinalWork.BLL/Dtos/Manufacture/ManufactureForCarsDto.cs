using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WEBAPI_FinalWork.DAL.Entities;

namespace WEBAPI_FinalWork.BLL.Dtos.Manufacture
{
    public class ManufactureForCarsDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public CountryEntity? Country { get; set; }
    }
}
