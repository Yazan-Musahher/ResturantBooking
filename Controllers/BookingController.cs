using System.Dynamic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Resturant_Booking.Data;
using Resturant_Booking.Models;
using TwilioTest.Data;

namespace Resturant_Booking.Controllers;

public class BookingController : Controller
{
    private ApplicationDbContext _db;
    private UserManager<ApplicationUser> _um;
    private readonly MessageService _messageService = new MessageService();
    
    public BookingController(ApplicationDbContext db, UserManager<ApplicationUser> um)
    {
        _db = db;
        _um = um;
    }
    
    [Authorize]
    public IActionResult Index(string message = "")
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
        //myModel.Message = message;
        
        return View(myModel);
    }

    [Authorize]
    [HttpPost]
    public IActionResult Index(Reservation reservation, int seats, int restaurantId)
    {
        // Find tables
        var tables = _db.Tables
            .Where(i => i.RestaurantId == restaurantId) // Only tables at selected restaurant
            .Where(i => i.Seats >= seats)               // Only tables with enough seats
            .OrderBy(i => i.Seats);                     // Ordered from closest to least close in seats
        
            
        var restaurant = _db.Restaurants.Find(restaurantId);

        // Find first available 
        foreach (var table in tables)
        {
            // Check if table is available
            var reservations = _db.Reservations
                .Where(i => i.TableId == table.TableId)
                .Where(i => i.Time >= reservation.Time || i.Time <= reservation.Time.AddHours(1));

            // If an available table is found
            if (!reservations.Any())
            {
                reservation.Table = table;
                break;
            }
        }

        // If no table was found
        if (reservation.Table == null)
        {
            //ViewBag.Message = "No available tables";
            ViewData["Message"] = "No available tables";
            return RedirectToAction("Index", new { message = "No available tables" });
        }
        
        
        // Add user to reservation
        reservation.User = _um.GetUserAsync(User).Result;

        // Add to database and save changes
        _db.Reservations.Add(reservation);
        _db.SaveChanges();
        
        // Return to view of user reservations if we make that
        //ViewBag.Message = "Table reserved";
        ViewData["Message"] = "Table reserved";
        
        // Sends out a confirmation text message
        if (restaurant != null)
        {
            Console.WriteLine("Message sent");
            _messageService.SendMessage(restaurant, reservation);
        }
            
        
        return RedirectToAction("Index", new { message = "Table reserved" });
    }
}