using DataAccessLayer.Entities;
using DataAccessLayer.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository
{
    public abstract class GenericRepository<T>(DbContext db) : IGenericRepository<T> where T : BaseEntity
    {
        public async Task<int> CreateAsync(T entity)
        {
            var dbSet = db.Set<T>();
            if (entity.Id == 0)
            {
                await dbSet.AddAsync(entity);
                return await db.SaveChangesAsync();
            }
            else
            {
                db.Entry(entity).State = EntityState.Modified;
                return await db.SaveChangesAsync();
            }
        }

        public async Task<int> DeleteAsync(Expression<Func<T, bool>> specification)
        {
            var dbSet = db.Set<T>();
            var entitiesToDelete = await dbSet.Where(specification).ToListAsync();
            dbSet.RemoveRange(entitiesToDelete);
            return await db.SaveChangesAsync();
        }

        public async Task<int> DeleteByIdAsync(int id)
        {
            return await DeleteAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<T>> GetAsync()
        {
            return await GetAsync(s => true);
        }

        public async Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> predicate)
        {
            var dbSet = db.Set<T>();
            return await dbSet.AsQueryable().Where(predicate).ToListAsync();
        }
    }
}
