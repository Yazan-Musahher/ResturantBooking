using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Resturant_Booking.Data;
using Resturant_Booking.Models;

namespace Resturant_Booking.Controllers;

public class RestaurantRegistrationController : Controller
{
    private readonly ApplicationDbContext _db;

    public RestaurantRegistrationController(ApplicationDbContext db)
    {
        _db = db;
    }
    
    [HttpGet]
    public IActionResult Index()
    {
        return View(new Restaurant());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Index(Restaurant restaurant)
    {
        if (!ModelState.IsValid)
        {
            return View(restaurant);
        }

        _db.Restaurants.Add(restaurant);
        _db.SaveChanges();

        return RedirectToAction(nameof(Index));
    }

}