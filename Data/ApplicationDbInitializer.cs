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
        
        var user2 = new ApplicationUser()
            { UserName = "user2@uia.no", Email = "user2@uia.no", Name = "User2", EmailConfirmed = true };

        um.CreateAsync(admin, "Password1.").Wait();
        um.AddToRoleAsync(admin, "Admin").Wait();
        
        um.CreateAsync(user, "Password1.").Wait();
        um.CreateAsync(user2, "Password1.").Wait();
        
        // Add example restaurant
        var restaurants = new[]
        {
            new Restaurant("Restaurant1", "Address1", "Grimstad","Email1", "12345678"),
            new Restaurant("Restaurant2", "Address2", "Grimstad", "Email2","87654321"),
            new Restaurant("Restaurant3", "Address3", "Kristaiansand","Email3", "69696969")
        };
        
        db.Restaurants.AddRange(restaurants);
        
        // Add relationship between users and restaurants
        admin.Restaurant = restaurants[0];
        user.Restaurant = restaurants[1];
        
        // Add example Tables
        var tables = new[]
        {
            new Table(4),
            new Table(4),
            new Table(5),
            new Table(2),
            new Table(3),
            new Table(5),
        };
        
        db.Tables.AddRange(tables);
        
        // Add relationships to restaurants
        tables[0].Restaurant = restaurants[0];
        tables[1].Restaurant = restaurants[1];
        tables[2].Restaurant = restaurants[2];
        tables[3].Restaurant = restaurants[0];
        tables[4].Restaurant = restaurants[1];
        tables[5].Restaurant = restaurants[2];

        // Add example reservations
        var reservations = new[]
        {
            new Reservation(DateTime.Now.AddHours(1)),
            new Reservation(DateTime.Now.AddHours(2)),
            new Reservation(DateTime.Now.AddHours(3)),
            new Reservation(DateTime.Now.AddHours(4))
        };
        
        db.Reservations.AddRange(reservations);

        reservations[0].Table = tables[0];
        reservations[1].Table = tables[1];
        reservations[2].Table = tables[2];
        reservations[3].Table = tables[0];
        
        reservations[0].User = user;
        reservations[1].User = user;
        reservations[2].User = admin;
        reservations[3].User = admin;
        
        // Add example dishes
        var dishes = new[]
        {                               // Price in øre
            new Dish("Dish name 1", 10000, restaurants[0]),
            new Dish("Dish name 2", 15000, restaurants[0]),
            new Dish("Dish name 3", 15000, restaurants[1]),
            new Dish("Dish name 4", 20000, restaurants[1]),
            new Dish("Dish name 5", 20000, restaurants[2]),
            new Dish("Dish name 6", 25000, restaurants[2])
        };
        
        db.Dishes.AddRange(dishes);

        // Save changes made to database
        db.SaveChanges();
        
    }
}