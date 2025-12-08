using Microsoft.EntityFrameworkCore;
using RestaurantManager.Data;

namespace RestaurantManager.Models
{
    public static class SeedCustomer
    {


        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new RestaurantManagerContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<RestaurantManagerContext>>()))
            {
                // Look for any customers.
                if (context.Customer.Any())
                {
                    return;   // DB has been seeded
                }
                context.Customer.AddRange(
                    new Customer
                    {
                        Name = "Alice Johnson",
                        Email = "Alice@Johnson.com",
                        PhoneNumber = "555-1234"
                    },
                    new Customer
                    {
                        Name = "Bob Smith",
                        Email = "Bob@Smith.com",
                        PhoneNumber = "555-5678"
                    },
                    new Customer
                    {
                        Name = "Charlie Brown",
                        Email = "Charlie@Brown.com",
                        PhoneNumber = "555-8765"
                    },
                    new Customer
                    {
                        Name = "Diana Prince",
                        Email = "Diana@Prince.com",
                        PhoneNumber = "555-4321"
                    },
                    new Customer
                    {
                        Name = "Ethan Hunt",
                        Email = "Ethan@Hunt.com",
                        PhoneNumber = "555-2468",
                    }
            );
                context.SaveChanges();
            }
        }
    }


}