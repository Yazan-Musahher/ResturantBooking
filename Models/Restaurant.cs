using System.ComponentModel.DataAnnotations;

namespace Resturant_Booking.Models;

public class Restaurant
{
    public Restaurant() {}

    public Restaurant(string name, string address, string city,string email, string phoneNumber)
    {
        Name = name;
        Address = address;
        City = city;
        Email = email;
        PhoneNumber = phoneNumber;
    }
        
    [Required]
    public int RestaurantId { get; set; }

    [Required]
    [StringLength(200)]
    public string Name { get; set; } = String.Empty;
    
    [Required]
    [StringLength(200)]
    public string Address { get; set; } = String.Empty;
    
    [Required]
    [StringLength(200)]
    public string City { get; set; } = String.Empty;
    
    [Required]
    [StringLength(200)]
    public string Email { get; set; } = String.Empty;
    
    [Required]
    [StringLength(20)]
    public string PhoneNumber { get; set; } = String.Empty;
}