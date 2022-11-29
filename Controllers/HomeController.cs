using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Resturant_Booking.Data;
using Resturant_Booking.Models;

namespace Resturant_Booking.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    
       
    private ApplicationDbContext _db;

    public HomeController(ILogger<HomeController> logger, ApplicationDbContext db)
    {
        _logger = logger;
        _db = db;
    }

    public IActionResult Index()
    {
        //_db.Restaurants;
        return View(_db.Restaurants);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}