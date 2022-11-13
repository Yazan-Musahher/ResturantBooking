using Microsoft.AspNetCore.Identity;
using Microsoft.Build.Framework;

namespace Resturant_Booking.Models;

public class ApplicationUser : IdentityUser
{
    [Required]
    public string Name { get; set; } = String.Empty;
    
    
    // Foreign key
    public int? RestaurantId { get; set; }
    public Restaurant? Restaurant { get; set; }
}