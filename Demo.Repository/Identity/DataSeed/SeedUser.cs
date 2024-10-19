using Demo.Core.Models.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Repository.Identity.DataSeed
{
    public static class SeedUser
    {
        public static async Task SeedUserDataAsync(UserManager<AppUser> _userManager, RoleManager<IdentityRole> _roleManager)
        {
            if (_userManager.Users.Count() == 0)
            {

                string email = "Hossamdev9@gmail.com";
                string password = "Admin@123";
                
                var existingUser = await _userManager.FindByEmailAsync(email);
                if (existingUser != null)
                {
                    Console.WriteLine("User with email already exists.");
                    return;
                }


                var newUser = new AppUser()
                {
                    Email = email,
                    DisplayName = "Hossam_H",
                    UserName = "Hossam_Hassan",
                    PhoneNumber = "01152183282",
                    Address = new Address()
                    {
                        FName = "Hossam",
                        LName = "Hassan",
                        City = "Cairo",
                        Country = "Egypt",
                        Street = "Marg"
                    }
                };
                if (!await _roleManager.RoleExistsAsync("Admin"))
                {
                    await _roleManager.CreateAsync(new IdentityRole("Admin"));
                }
                var result = await _userManager.CreateAsync(newUser, password);
                
                if (result.Succeeded)
                {
                    Console.WriteLine("User created successfully.");
                   await _userManager.AddToRoleAsync(newUser, "Admin");
                }
                else
                {
                    Console.WriteLine("Failed to create user: " + string.Join(", ", result.Errors));
                }

            }
        }
    }
}

