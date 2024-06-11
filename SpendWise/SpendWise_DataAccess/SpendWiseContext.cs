using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpendWise_DataAccess.Entities;
using SpendWise_DataAccess.Configuration;

namespace SpendWise_DataAccess
{
    public class SpendWiseContext : DbContext
    {
        public SpendWiseContext(DbContextOptions<SpendWiseContext> options) : base(options) {

        }

        public DbSet<Category> categories { get; set; }
        public DbSet<Product> products { get; set; }

        public DbSet<Cart> carts { get; set; }

        public DbSet<CartProduct> cartProducts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            new CategoryConfiguration().Configure(modelBuilder.Entity<Category>());
            new ProductConfiguration().Configure(modelBuilder.Entity<Product>());
            new CartConfiguration().Configure(modelBuilder.Entity<Cart>());
            new CartProductConfiguration().Configure(modelBuilder.Entity<CartProduct>());
        }

    }
}
