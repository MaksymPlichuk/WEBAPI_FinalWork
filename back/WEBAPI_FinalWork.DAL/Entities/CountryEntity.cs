using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WEBAPI_FinalWork.DAL.Entities
{
    public class CountryEntity : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public List<string> ISOCode { get; set; } = [];
        public string? Image { get; set; }
        public List<ManufacturerEntity> Manufacturers { get; set; } = [];
    }
}
