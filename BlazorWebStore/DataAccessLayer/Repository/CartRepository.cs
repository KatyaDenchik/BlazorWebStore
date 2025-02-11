using DataAccessLayer.Entities;
using DataAccessLayer.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository
{
    public class CartRepository : GenericRepository<CartItemEntity>, ICartRepository
    {
        public CartRepository(DbContext dbContext) : base(dbContext)
        {

        }
    }
}
