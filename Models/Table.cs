using System.ComponentModel.DataAnnotations;

namespace Resturant_Booking.Models;

public class Table
{
    public Table(){}

    public Table(int seats)
    {
        Seats = seats;
    }

    [Required]
    public int TableId { get; set; }

    [Required] public int Seats { get; set; } = 1;
    
    
    // Foreign keys
    public int? RestaurantId { get; set; }
    public Restaurant? Restaurant { get; set; }

}