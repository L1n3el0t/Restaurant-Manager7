using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RestaurantManager.Models;

namespace RestaurantManager.Data
{
    public class RestaurantManagerContext : DbContext
    {
        public RestaurantManagerContext (DbContextOptions<RestaurantManagerContext> options)
            : base(options)
        {
        }

        public DbSet<RestaurantManager.Models.Customer> Customer { get; set; } = default!;
        public DbSet<RestaurantManager.Models.Reservation> Reservation { get; set; } = default!;
    }
}
