using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using Resturant_Booking.Controllers;
using Resturant_Booking.Data;
using Resturant_Booking.Models;

namespace Resturant_Booking
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        private UserManager<IdentityUser> _userManager;
        
        
        public AccountController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        
        [HttpGet]
        [Route("User")]


        //get all users from our database

        public IActionResult GetUser()
        {
            return Ok(dbContext.Users.ToList());
        }
        
        // Add new users to our database
       
        
        
        
        
        
       
    }
}
