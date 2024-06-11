using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpendWise_DataAccess.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SpendWise_DataAccess.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Product").HasKey(p=>p.Id);

            builder.Property(p=>p.Name).HasMaxLength(255);

            builder.HasMany(p => p.Categories).WithMany(c => c.Products)
                .UsingEntity<Dictionary<string, object>>("ProductCategoty",
                e => e.HasOne<Category>().WithMany().HasForeignKey("CategoryId").HasConstraintName("FK_ProductCategory_Category"),
                e=>e.HasOne<Product>().WithMany().HasForeignKey("ProductId").HasConstraintName("FK_ProductCategory_Product"));
        }
    }
}
