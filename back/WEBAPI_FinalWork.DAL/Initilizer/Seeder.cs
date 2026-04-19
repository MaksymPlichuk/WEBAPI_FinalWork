using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WEBAPI_FinalWork.DAL.Entities;

namespace WEBAPI_FinalWork.DAL.Initilizer
{
    public static class Seeder
    {
        public static async Task Seed(this IApplicationBuilder app)
        {
            var scope = app.ApplicationServices.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            await context.Database.MigrateAsync();

            if (!context.Countries.Any())
            {
                var countries = new List<CountryEntity>()
                {
                    new CountryEntity() {
                        Name = "USA",
                        ISOCode = new List<string>(){"USA"},
                        Image = "https://upload.wikimedia.org/wikipedia/en/thumb/a/a4/Flag_of_the_United_States.svg/500px-Flag_of_the_United_States.svg.png"
                    },
                    new CountryEntity() {
                        Name = "Germany",
                        ISOCode = new List<string>(){"GER"},
                        Image = "https://upload.wikimedia.org/wikipedia/en/thumb/b/ba/Flag_of_Germany.svg/500px-Flag_of_Germany.svg.png"
                    },
                    new CountryEntity() {
                        Name = "UK",
                        ISOCode = new List<string>(){"ENG"},
                        Image = "https://upload.wikimedia.org/wikipedia/en/thumb/a/ae/Flag_of_the_United_Kingdom.svg/500px-Flag_of_the_United_Kingdom.svg.png"
                    }
                };
                await context.AddRangeAsync(countries);
                await context.SaveChangesAsync();
            }

            if (!context.Cars.Any())
            {
                var cars = new List<CarEntity>()
                {
                    new CarEntity()
                    {
                        Name = "Mercedes-Benz S-Class (W140)",
                        Year = 1991,
                        Volume = 6.0f,
                        Price = 25000,
                        Description = "The legendary '600 Sel' - a symbol of engineering excellence and luxury.",
                        Image = "MercedesW140.jpg",
                        Manufacturer = new ManufacturerEntity()
                        {
                            Name = "Mercedes-Benz",
                            Description = "A German luxury automotive brand known for high-quality vehicles.",
                            NumberOfWorkers = 170000,
                            FoundedDate = new DateTime(1926, 6, 28).ToUniversalTime(),
                            CountryId = 2
                        }
                    },
                    new CarEntity()
                    {
                        Name = "Ford Mustang Boss 429",
                        Year = 1969,
                        Volume = 7.0f,
                        Price = 280000,
                        Description = "A high-performance variant of the Ford Mustang, a true American muscle car icon.",
                        Image = "FordMustangBoss429.jpg",
                        Manufacturer = new ManufacturerEntity()
                        {
                            Name = "Ford Motor Company",
                            Description = "American multinational automobile manufacturer founded by Henry Ford.",
                            NumberOfWorkers = 180000,
                            FoundedDate = new DateTime(1903, 6, 16).ToUniversalTime(),
                            CountryId = 1
                        }
                    },
                    new CarEntity()
                    {
                        Name = "Bentley Continental GT",
                        Year = 2023,
                        Volume = 4.0f,
                        Price = 240000,
                        Description = "The definitive grand tourer, combining phenomenal performance with exquisite craftsmanship.",
                        Image = "BentleyContinentalGT2023.jpg",
                        Manufacturer = new ManufacturerEntity()
                        {
                            Name = "Bentley Motors",
                            Description = "British manufacturer and marketer of luxury cars and SUVs.",
                            NumberOfWorkers = 4000,
                            FoundedDate = new DateTime(1919, 1, 18).ToUniversalTime(),
                            CountryId = 3
                        }
                    }
                };
                await context.AddRangeAsync(cars);
                await context.SaveChangesAsync();
            }
        }
    }
}
