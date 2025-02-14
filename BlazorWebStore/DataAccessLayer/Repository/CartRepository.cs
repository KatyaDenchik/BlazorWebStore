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
    public class CartRepository : GenericRepository<CartItemEntity>, ICartRepository
    {
        public CartRepository(DbContext dbContext) : base(dbContext)
        {

        }


        public override async Task<IEnumerable<CartItemEntity>> GetAsync(Expression<Func<CartItemEntity, bool>> predicate)
        {
            IQueryable<CartItemEntity> query = db.Set<CartItemEntity>();

            var entityType = db.Model.FindEntityType(typeof(CartItemEntity));
            var navigationProperties = entityType.GetNavigations().Select(n => n.Name);

            foreach (var navigationProperty in navigationProperties)
            {
                query = query.Include(navigationProperty);
            }

            return await query.Where(predicate).ToListAsync();
        }
    }
}
