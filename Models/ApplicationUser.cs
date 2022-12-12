using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Resturant_Booking.Models;

public class ApplicationUser : IdentityUser
{
    [Microsoft.Build.Framework.Required]
    public string Name { get; set; } = String.Empty;
    
    [StringLength(20)]
    public override string Email { get; set; }
    
    public override string PasswordHash { get; set; }
    
    
    
    
    // Foreign key
    public int? RestaurantId { get; set; }
    public Restaurant? Restaurant { get; set; }
}