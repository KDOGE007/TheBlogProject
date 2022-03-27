using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheBlogProject.Data;
using TheBlogProject.Enums;
using TheBlogProject.Models;

namespace TheBlogProject.Services
{
    public class DataService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<BlogUser> _userManager;

        public DataService(ApplicationDbContext dbContext, RoleManager<IdentityRole> roleManager, UserManager<BlogUser> userManager)
        {
            _dbContext = dbContext;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task ManageDataAsync()
        {
            //Task 0: Create the DB from the Migrations
            await _dbContext.Database.MigrateAsync();

            //Task 1: Seeding a few Roles into the system
            await SeedRolesAsync();

            //Task 2: seed a user into the system
            await SeedUsersAsync();
        }

        private async Task SeedRolesAsync()
        {
            //If there are already Roles in the system, do nothing.
            if (_dbContext.Roles.Any()) 
            {
                return;
            }

            //Otherwise we want to creat a few Roles
            foreach(var role in Enum.GetNames(typeof(BlogRole)))
            {
                //I need to use the Role manager to create roles
                await _roleManager.CreateAsync(new IdentityRole(role));
            }
        }

        private async Task SeedUsersAsync()
        {
            //If there are already User in the system, do nothing.
            if (_dbContext.Users.Any())
            {
                return;
            }

            //step 1: creates a new instance of BlogUser
            var adminUser = new BlogUser()
            {
                Email = "kharnthorn@gmail.com",
                UserName = "kharnthorn@gmail.com",
                DisplayName = "KFC001",
                FirstName = "Kharn",
                LastName = "Tupmongkol",                
                PhoneNumber = "987654321",
                EmailConfirmed = true,               
            };

            //Step 2: Use the UserManager to create a new user that is defined by adminUser
            await _userManager.CreateAsync(adminUser, "Abc&123!");

            //Step 3: Add this new user to the Administrator role
            await _userManager.AddToRoleAsync(adminUser, BlogRole.Administrator.ToString());

            var modUser = new BlogUser()
            {
                Email = "kcode.001@google.com",
                UserName = "kcode.001@google.com",
                DisplayName = "McDonald 001",
                FirstName = "Ted",
                LastName = "Baker",
                PhoneNumber = "0123456789",
                EmailConfirmed = true,
            };

            await _userManager.CreateAsync(modUser, "Abc&123!");
            await _userManager.AddToRoleAsync(modUser, BlogRole.Moderator.ToString());

        }

    }
}
