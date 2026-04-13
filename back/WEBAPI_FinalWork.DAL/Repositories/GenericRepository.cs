using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WEBAPI_FinalWork.DAL.Entities;

namespace WEBAPI_FinalWork.DAL.Repositories
{
    public abstract class GenericRepository<TEntity> where TEntity : class, IBaseEntity
    {
        private readonly AppDbContext _context;
        protected GenericRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateAsync(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
            return (await _context.SaveChangesAsync() != 0);
        }
        public async Task<bool> CreateRangeAsync(params TEntity[] entities)
        {
            foreach (var entity in entities)
            {
                await _context.Set<TEntity>().AddAsync(entity);
            }
            return (await _context.SaveChangesAsync() != 0);
        }
        public async Task<bool> UpdateAsync(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
            return (await _context.SaveChangesAsync() != 0);
        }

        public async Task<bool> DeleteAsync(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
            return (await _context.SaveChangesAsync() != 0);
        }
        public async Task<bool> DeleteByIdAsync(int Id)
        {
            var res = await GetByIdAsync(Id);
            if (res != null)
            {
                _context.Set<TEntity>().Remove(res);
                return (await _context.SaveChangesAsync() != 0);
            }
            return false;
        }

        public async Task<TEntity?> GetByIdAsync(int Id)
        {
            return await _context.Set<TEntity>().FirstOrDefaultAsync(x => x.Id == Id);
        }
        public IQueryable<TEntity> GetAll()
        {
            return _context.Set<TEntity>().AsNoTracking();
        }
    }
}
