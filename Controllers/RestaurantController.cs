﻿using System.Dynamic;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Resturant_Booking.Data;
using Resturant_Booking.Models;

using Microsoft.AspNetCore.Identity;



namespace Resturant_Booking.Controllers;

public class RestaurantController : Controller
{
    private ApplicationDbContext _db;
    private UserManager<ApplicationUser> _um;
    
    public RestaurantController(ApplicationDbContext db, UserManager<ApplicationUser> um)
    {
        _db = db;
        _um = um;
    }
    
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
        
        // Check that user has access to restaurant
        var applicationUser = _um.GetUserAsync(User).Result;
        if (restaurantId != applicationUser.RestaurantId && !User.IsInRole("Admin"))
        {
            return RedirectToRoute("default", 
                new { controller = "Restaurant", action = "Index", id = applicationUser.RestaurantId.ToString() });
        }

        // Get all reservations to restaurant
        var reservations = _db.Reservations
            .Include(i => i.Table.Restaurant)
            .Include(i => i.User)
            .Where(i => i.Table.RestaurantId == restaurantId)
            .OrderBy(i => i.Time);
        
        dynamic myModel = new ExpandoObject();
        myModel.Reservations = reservations;
        myModel.Restaurant = _db.Restaurants.Find(restaurantId);
        
        return View(myModel);
    }
    
    [HttpGet]
    [Authorize]
    public IActionResult Edit()
    {
        // Get restaurant id
        var restaurantIdString = Url.ActionContext.RouteData.Values["id"]?.ToString();
        var restaurantId = 0;
        if (restaurantIdString != null)
        {
            restaurantId = Int32.Parse(restaurantIdString);
        }
        
        // Check that user has access to restaurant
        var applicationUser = _um.GetUserAsync(User).Result;
        if ((restaurantId != applicationUser.RestaurantId && !User.IsInRole("Admin")) || restaurantId == 0)
        {
            return RedirectToRoute("default", 
                new { controller = "Restaurant", action = "Edit", id = applicationUser.RestaurantId.ToString() });
        }
        
        // Get all reservations to restaurant
        var restaurant = _db.Restaurants.Find(restaurantId);
        
        dynamic myModel = new ExpandoObject();
        myModel.Restaurant = restaurant;
        myModel.Users = _db.Users.Where(i => i.RestaurantId == restaurantId);
        
        return View(myModel);
    }

    [HttpPost]
    [Authorize]
    public IActionResult Edit(string email)
    {
        // Find user by email
        var user = _db.Users.Where(i => i.Email == email).FirstOrDefault();
        
        // Check if user exists
        if (user == null)
        {
            //"User does not exist"
            return RedirectToAction("Edit");
        }

        // Check if user has a restaurant
        if (user.RestaurantId != null)
        {
            //"User already has a restaurant"
            return RedirectToAction("Edit");
        }
        
        // Get restaurant id
        var restaurantIdString = Url.ActionContext.RouteData.Values["id"]?.ToString();
        var restaurantId = 0;
        if (restaurantIdString != null)
        {
            restaurantId = Int32.Parse(restaurantIdString);
        }

        var restaurant = _db.Restaurants.Find(restaurantId);

        // Give user access to restaurant
        user.Restaurant = restaurant;
        _db.SaveChanges();

        return RedirectToAction("Edit");
        
    }

    /*
    [HttpPost]
    [Authorize]
    public IActionResult UpdateRestaurant(int id, Restaurant newRestaurant)
    {
        var restaurant = _db.Restaurants.Find(id);
        if (restaurant != null)
        {
            restaurant.Name = newRestaurant.Name;
            restaurant.Address = newRestaurant.Address;
            restaurant.Email = newRestaurant.Email;
            restaurant.PhoneNumber = newRestaurant.PhoneNumber;
            _db.SaveChanges();
        }

        return RedirectToAction("Edit", restaurant);
        
    }
    */
    
}