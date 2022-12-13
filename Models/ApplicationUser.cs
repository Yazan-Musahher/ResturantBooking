using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Resturant_Booking.Models;

public class ApplicationUser : IdentityUser
{
    [Microsoft.Build.Framework.Required]
    public string Name { get; set; } = String.Empty;
    
    


    // Foreign key
    public int? RestaurantId { get; set; }
    public Restaurant? Restaurant { get; set; }
}