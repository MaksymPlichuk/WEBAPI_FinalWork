using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WEBAPI_FinalWork.DAL.Entities;

namespace WEBAPI_FinalWork.DAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<CarEntity> Cars { get; set; }
        public DbSet<ManufacturerEntity> Manufacturers { get; set; }
        public DbSet<CountryEntity> Countries { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<CarEntity>(e =>
            {
                e.HasKey(c => c.Id);
                e.Property(c => c.Name).IsRequired();
                e.Property(c => c.Description).HasColumnType("text");
                e.Property(c => c.Volume).HasDefaultValue(0f);
                e.Property(c => c.Price).HasDefaultValue(0);
                e.Property(c => c.Image).HasMaxLength(200);

                e.HasOne(c => c.Manufacturer).WithMany(m => m.Cars).HasForeignKey(c => c.ManufacturerId);
            });
            builder.Entity<ManufacturerEntity>(e =>
            {
                e.HasKey(m => m.Id);
                e.Property(m => m.Name).IsRequired();
                e.Property(m => m.NumberOfWorkers).HasDefaultValue(0);
                e.Property(m => m.Description).HasColumnType("text");

                e.HasOne(m => m.Country).WithMany(co => co.Manufacturers).HasForeignKey(m => m.CountryId);
            });
            builder.Entity<CountryEntity>(e =>
            {
                e.HasKey(co => co.Id);
                e.Property(co => co.Name).IsRequired();
                e.Property(co => co.Image).HasMaxLength(200);
                e.HasIndex(co => co.Name).IsUnique();
            });

        }
    }
}
