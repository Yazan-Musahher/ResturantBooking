using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Resturant_Booking.Data;
using Resturant_Booking.Models;

namespace Resturant_Booking.Controllers;

public class RestaurantController : Controller
{
    private ApplicationDbContext _db;
    
    public RestaurantController(ApplicationDbContext db)
    {
        _db = db;
    }
    
    // GET
    [HttpGet]
    [Authorize]
    public IActionResult Index()
    {
        // Get restaurant id
        var restaurantIdString = Url.ActionContext.RouteData.Values["id"]?.ToString();
        var restaurantId = 0;
        if (restaurantIdString != null)
        {
            restaurantId = Int32.Parse(restaurantIdString);
        }

        //if(routeId != applicationUser.restaurant)
        // return ;
        
        
        // Get restaurant from db and pass to view

        var reservations = _db.Reservations
            .Include(i => i.Table.Restaurant)
            .Include(i => i.User)
            .Where(i => i.Table.RestaurantId == restaurantId)
            .OrderBy(i => i.Time);
        
        return View(reservations);
    }
}