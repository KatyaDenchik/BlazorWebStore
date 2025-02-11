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
    public class UserRepository : GenericRepository<UserEntity>, IUserRepository
    {
        public UserRepository(DbContext dbContext) : base(dbContext)
        {
            
        }
    }
}
