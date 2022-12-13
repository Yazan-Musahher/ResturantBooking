using Microsoft.Build.Framework;

namespace Resturant_Booking.Models;

public class Registrering
{
    [Required]
    public string UserName { get; set; }
    [Required]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
    
}