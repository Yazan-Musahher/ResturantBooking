namespace Resturant_Booking.Models;

public class Reservation
{
    public Reservation(){}

    public Reservation(DateTime time)
    {
        Time = time;
    }

    public int ReservationId { set; get; }

    public DateTime Time { set; get; } = DateTime.Now;
    
    // Foreign keys
    public int? TableId { get; set; }
    public Table? Table { get; set; }
    
    public String? UserId { get; set; }
    public ApplicationUser? User { get; set; }
}