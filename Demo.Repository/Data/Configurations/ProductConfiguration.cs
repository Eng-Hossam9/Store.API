using Demo.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Repository.Data.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(p => p.Name).IsRequired();
            builder.Property(p=>p.PictureUrl).IsRequired();
            builder.Property(p => p.Price).HasColumnType("decimal(18,2)");
            builder.HasOne(p=>p.Brand).WithMany().HasForeignKey(p=>p.BrandId).OnDelete(DeleteBehavior.SetNull);
            builder.HasOne(p=>p.Type).WithMany().HasForeignKey(p=>p.TypeId).OnDelete(DeleteBehavior.SetNull);
            builder.Property(p => p.TypeId).IsRequired(false);
            builder.Property(p => p.BrandId).IsRequired(false);

        }
    }
}
