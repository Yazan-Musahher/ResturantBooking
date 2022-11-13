using Resturant_Booking.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.WebEncoders.Testing;

namespace Resturant_Booking.Data;

public static class ApplicationDbInitializer
{
    public static void Initialize(ApplicationDbContext db, UserManager<ApplicationUser> um, RoleManager<IdentityRole> rm)
    {
        
        // Delete existing database
        db.Database.EnsureDeleted();

        // Create new database
        db.Database.EnsureCreated();
        
        // Create roles
        var adminRole = new IdentityRole("Admin");
        rm.CreateAsync(adminRole).Wait();
        
        
        // Add standard users
        var admin = new ApplicationUser()
            { UserName = "admin@uia.no", Email = "admin@uia.no", Name = "Admin", EmailConfirmed = true };
        
        var user = new ApplicationUser()
            { UserName = "user@uia.no", Email = "user@uia.no", Name = "User", EmailConfirmed = true };

        um.CreateAsync(admin, "Password1.").Wait();
        um.AddToRoleAsync(admin, "Admin").Wait();
        
        um.CreateAsync(user, "Password1.").Wait();
        
        // Add example restaurant
        var restaurants = new[]
        {
            new Restaurant("Restaurant1", "Address1", "Email1", "12345678"),
            new Restaurant("Restaurant2", "Address2", "Email2", "87654321"),
            new Restaurant("Restaurant3", "Address4", "Email3", "69696969")
        };
        
        db.Restaurants.AddRange(restaurants);
        
        // Add example Tables
        var tables = new[]
        {
            new Table(4),
            new Table(4),
            new Table(5)
        };
        
        db.Tables.AddRange(tables);
        
        // Add relationships to restaurants
        tables[0].Restaurant = restaurants[0];
        tables[1].Restaurant = restaurants[1];
        tables[2].Restaurant = restaurants[2];

        // Add example reservations
        var reservations = new[]
        {
            new Reservation(DateTime.Now),
            new Reservation(DateTime.Now),
            new Reservation(DateTime.Now)
        };
        
        db.Reservations.AddRange(reservations);

        reservations[0].Table = tables[0];
        reservations[1].Table = tables[1];
        reservations[2].Table = tables[2];
        
        reservations[0].User = user;
        reservations[1].User = user;
        reservations[2].User = admin;

        // Save changes made to database
        db.SaveChanges();
        
    }
}