using System.Dynamic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Resturant_Booking.Data;
using Resturant_Booking.Models;

namespace Resturant_Booking.Controllers;

public class BookingController : Controller
{
    private ApplicationDbContext _db;
    private UserManager<ApplicationUser> _um;
    
    public BookingController(ApplicationDbContext db, UserManager<ApplicationUser> um)
    {
        _db = db;
        _um = um;
    }
    public IActionResult Index()
    {
        // Get restaurant id
        var restaurantIdString = Url.ActionContext.RouteData.Values["id"]?.ToString();
        var restaurantId = 0;
        if (restaurantIdString != null)
        {
            restaurantId = Int32.Parse(restaurantIdString);
        }

        // Get restaurant from db
        var restaurant = _db.Restaurants.Find(restaurantId);
        // Get tables from database
        var tables = _db.Tables.Where(i => i.RestaurantId == restaurantId);
        // Get reservations
        var reservations = _db.Reservations.Where(i => i.Table.RestaurantId == restaurantId);

        dynamic myModel = new ExpandoObject();
        myModel.Restaurant = restaurant;
        myModel.Tables = tables;
        myModel.Reservations = reservations;
        
        return View(myModel);
    }

    [Authorize]
    [HttpPost]
    public IActionResult Index(Reservation reservation)
    {
        // Check that reservation does not overlap
        var overlap = _db.Reservations
            .Where(i => i.Table == reservation.Table)
            .Where(i => i.Time >= reservation.Time)
            .Where(i => i.Time < reservation.Time.AddHours(1)).FirstOrDefault();

        if (overlap != null)
        {
            ViewBag.Message = "Table is already reserved at that time";  
            return RedirectToAction("Index");
        }
        
        // Add user to reservation
        reservation.User = _um.GetUserAsync(User).Result;

        // Add to database and save changes
        _db.Reservations.Add(reservation);
        _db.SaveChanges();
        
        // Return to view of user reservations if we make that
        ViewBag.Message = "Table reserved";
        return RedirectToAction("Index");
    }
}