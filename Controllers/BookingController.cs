using Microsoft.AspNetCore.Mvc;

namespace Resturant_Booking.Controllers;

public class BookingController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}