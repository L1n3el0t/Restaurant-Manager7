using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Xunit;
using Xunit.Sdk;

namespace RestaurantManager.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public Customer? Customer { get; set; }

        [Display(Name = "Reservation Time")]
        [FutureDate(ErrorMessage = "Reservation time must be in the future.")]
        public DateTime ReservationTime { get; set; }

        [Required]
        [Range(1, 100)]
        [Display(Name = "Number of Guests")]
        public int NumberOfGuests { get; set; }

        [Display(Name = "Special Requests (optional)")]
        public string? SpecialRequests { get; set; }
    }
}
