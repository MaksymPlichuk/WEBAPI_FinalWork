using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WEBAPI_FinalWork.DAL.Entities
{
    public class ManufacturerEntity : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int NumberOfWorkers { get; set; } = 0;
        public DateTime FoundedDate { get; set; } = DateTime.UtcNow;
        public int CountryId { get; set; }
        public CountryEntity? Country { get; set; }
        public List<CarEntity> Cars { get; set; }
    }
}
