using Microsoft.Build.Framework;

namespace Resturant_Booking.Models;

public class Dish
{
    public Dish(){}

    public Dish(string name, int price)
    {
        Name = name;
        Price = price;
    }
    
    public Dish(string name, int price, Restaurant restaurant)
    {
        Name = name;
        Price = price;
        Restaurant = restaurant;
    }

    [Required]
    public int DishId { get; set; }

    [Required]
    public string Name { get; set; } = String.Empty;
    
    // Price is in øre to get precise prize while avoiding floating point error.
    [Required]
    public int Price { get; set; } = 0;
    
    // Foreign keys
    public int? RestaurantId { get; set; }
    public Restaurant? Restaurant { get; set; }
}