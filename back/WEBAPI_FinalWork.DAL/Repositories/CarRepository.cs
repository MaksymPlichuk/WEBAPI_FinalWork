using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WEBAPI_FinalWork.DAL.Entities;

namespace WEBAPI_FinalWork.DAL.Repositories
{
    public class CarRepository : GenericRepository<CarEntity>
    {
        private readonly AppDbContext _context;
        public CarRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<CarEntity?> GetByNameAsync(string name)
        {
            return await GetAll().FirstOrDefaultAsync(c => c.Name == name);
        }
    }
}
