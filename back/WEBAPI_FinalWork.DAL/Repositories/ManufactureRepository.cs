using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WEBAPI_FinalWork.DAL.Entities;

namespace WEBAPI_FinalWork.DAL.Repositories
{
    public class ManufactureRepository : GenericRepository<ManufacturerEntity>
    {
        public ManufactureRepository(AppDbContext context) : base(context)
        {
        }
        public async Task<ManufacturerEntity?> GetByNameAsync(string name)
        {
            return await GetAll().FirstOrDefaultAsync(m => m.Name == name);
        }
    }
}
