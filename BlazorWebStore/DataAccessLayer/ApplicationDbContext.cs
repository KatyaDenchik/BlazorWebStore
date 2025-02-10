using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class ApplicationDbContext : DbContext
    {
        private static bool firstConnection = true;
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        {
            if (firstConnection)
            {
                Database.Migrate();
                firstConnection = false;
            }
        }

        public DbSet<UserEntity> Users { get; set; }
        public DbSet<ProductEntity> Products { get; set; }
        public DbSet<CartItemEntity> CartItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ProductEntity>()
                .Property(p => p.Price)
                .HasPrecision(18, 2);

            modelBuilder.Entity<UserEntity>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<UserEntity>()
                .HasMany(u => u.CartItems)
                .WithOne(c => c.User)
                .HasForeignKey(c => c.UserId);

            modelBuilder.Entity<ProductEntity>()
                .HasMany(p => p.CartItems)
                .WithOne(c => c.Product)
                .HasForeignKey(c => c.ProductId);
        }

    }
}
