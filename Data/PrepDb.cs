using System;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using PlatformService.Models;

namespace PlatformService.Data
{
    /// <summary>
    /// Class that manages database seeding and migrations
    /// </summary>
    public static class PrepDb
    {
        public static void PrepPopulation(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>());
            }
        }

        private static void SeedData(AppDbContext dbContext)
        {
            if (!dbContext.Platforms.Any())
            {
                Console.WriteLine("--> Seeding data");
                dbContext.Platforms.AddRange(
                    new Platform {Name = "Dot Net", Publisher = "Microsoft", Cost = "Free"},
                    new Platform {Name = "SQL Server Express", Publisher = "Microsoft", Cost = "Free"},
                    new Platform {Name = "Kubernetes", Publisher = "Cloud Native Computing Foundation", Cost = "Free"}
                );

                dbContext.SaveChanges();
            }
            else
            {
                Console.WriteLine("--> There is data in the database already. Skipping seeding");
            }
        }
    }
}