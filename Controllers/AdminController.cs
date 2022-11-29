using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Resturant_Booking.Data;
using Resturant_Booking.Models;

namespace Resturant_Booking.Controllers;

public class AdminController : Controller
{
    
    private ApplicationDbContext _db;
    
    public AdminController(ApplicationDbContext db)
    {
        _db = db;
    }
    
    [HttpGet]
    [Authorize(Roles = "Admin")]
    public IActionResult Index()
    {
        return View();
    }
    
    // Add new restaurant
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public IActionResult Index(Restaurant restaurant, string userEmail)
    {
        // Get user from database
        var user = _db.Users.Where(i => i.Email == userEmail).FirstOrDefault();
        if (user == null)
        {
            // Error user does not exist
            return View();
        }
        
        // Add user and restaurant to database
        user.Restaurant = restaurant;
        _db.Restaurants.Add(restaurant);
        _db.SaveChanges();

        return View();
    }
}