using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Resturant_Booking.Data;
using Resturant_Booking.Models;

namespace Resturant_Booking.Controllers;

public class SearchResultController : Controller
{
    private readonly ApplicationDbContext _db;
    
    public SearchResultController(ApplicationDbContext db)
    {
        _db = db;
    }
    
    [HttpGet]
    // Documentation from Microsoft on search bar
    // https://learn.microsoft.com/en-us/aspnet/core/tutorials/first-mvc-app/search?view=aspnetcore-7.0
    public async Task<IActionResult> Index(string searchCity)
    {
        // Checks if database does not have any restaurants
        if (_db.Restaurants == null)
        {
            return Problem("There are no restaurants in the database.");
        }

        // Gets restaurants from the database
        var restaurants = from r in _db.Restaurants
            select r;

        //Chekcs if search string is empty
        if (!String.IsNullOrEmpty(searchCity))
        {
            // Finds restaurants which has the searched string as substring
            restaurants = restaurants.Where(res => res.Address.Contains(searchCity));
        }

        return View(await restaurants.ToListAsync());
    }
}