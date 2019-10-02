using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace PetStore.Models
{
    public static class SeedData
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            ApplicationDbContext context = app.ApplicationServices
                .GetRequiredService<ApplicationDbContext>();
            context.Database.Migrate();
            if (!context.Products.Any())
            {
                context.Products.AddRange(
                    new Product
                    {
                        Name = "Cage for a parrot",
                        Description = "Big cage for a parrot",
                        Category = "Cages",
                        Price = 250
                    },
                    new Product
                    {
                        Name = "Cage for a hamster",
                        Description = "Medium cage for a hamster",
                        Category = "Cages",
                        Price = 170
                    },
                    new Product
                    {
                        Name = "Drinking bowl for a parrot",
                        Description = "Attaches to the bars of the cage",
                        Category = "Cage Accessories",
                        Price = 25
                    },
                    new Product
                    {
                        Name = "Feeder for a parrot",
                        Description = "Attaches to the bars of the cage",
                        Category = "Cage Accessories",
                        Price = 25
                    },
                    new Product
                    {
                        Name = "Food for budgerigars",
                        Description = "Vitamin food for budgies",
                        Category = "Food",
                        Price = 55
                    },
                    new Product
                    {
                        Name = "Food for large parrots",
                        Description = "Vitamin food for large parrots",
                        Category = "Food",
                        Price = 60
                    },
                    new Product
                    {
                        Name = "Food for medium parrots",
                        Description = "Vitamin food for large parrots",
                        Category = "Food",
                        Price = 55
                    },
                    new Product
                    {
                        Name = "Dry food for cats",
                        Description = "Grainless adult cat food with fresh chicken and potatoes",
                        Category = "Food",
                        Price = 115
                    },
                    new Product
                    {
                        Name = "Canned food for dogs",
                        Description = "For small dogs",
                        Category = "Food",
                        Price = 45
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
