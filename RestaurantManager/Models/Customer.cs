using System.ComponentModel.DataAnnotations;

namespace RestaurantManager.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [StringLength(50, MinimumLength = 1, ErrorMessage = "Name must be between 1 and 50 characters.")]
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        [Display(Name = "Phone Number")]
        [Phone(ErrorMessage = "Invalid phone number format.")]
        public string PhoneNumber { get; set; } = string.Empty;
    }
}
