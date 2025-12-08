using Microsoft.EntityFrameworkCore;
using RestaurantManager.Data;

namespace RestaurantManager.Models
{
    public static class SeedReservations
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new RestaurantManagerContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<RestaurantManagerContext>>()))
            {
                // Look for any reservations.
                if (context.Reservation.Any())
                {
                    return;   // DB has been seeded
                }
                context.Reservation.AddRange(
                    new Reservation
                    {
                        CustomerId = 1,
                        ReservationTime = DateTime.Now.AddDays(1).AddHours(19),
                        NumberOfGuests = 4,
                        SpecialRequests = "Window seat",

                    },
                    new Reservation
                    {
                        CustomerId = 2,
                        ReservationTime = DateTime.Now.AddDays(2).AddHours(20),
                        NumberOfGuests = 2,
                        SpecialRequests = "Vegan options",

                    },
                    new Reservation
                    {
                        CustomerId = 3,
                        ReservationTime = DateTime.Now.AddDays(3).AddHours(18),
                        NumberOfGuests = 6,
                        SpecialRequests = "Birthday celebration",
   
                    },
                    new Reservation
                    {
                        CustomerId = 4,
                        ReservationTime = DateTime.Now.AddDays(1).AddHours(21),
                        NumberOfGuests = 3,
                        SpecialRequests = "Quiet area",
                
                    },
                    new Reservation
                    {
                        CustomerId = 5,
                        ReservationTime = DateTime.Now.AddDays(4).AddHours(19),
                        NumberOfGuests = 5,
                        SpecialRequests = "High chair needed",
                
                    }
                );
                context.SaveChanges();
            }
        }


    }
}
