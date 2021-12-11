﻿using Dotnet_frameworks_project.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Dotnet_frameworks_project.Seeders
{

    public class SeedDatabase
    {
        public static void Initialize(IServiceProvider serviceProvider, UserManager<ApplicationUser> userManager)
        {
            using (var context = new ApplicationContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<ApplicationContext>>()))
            {
                ApplicationUser user1 = null;
                context.Database.EnsureCreated();

                if(!context.Users.Any())
            {
                    user1 = new ApplicationUser
                    {
                        UserName = "Admin",
                        Firstname = "Antoine",
                        Lastname = "Couck",
                        Email = "System.administrator@studentenbeheer.be",
                        EmailConfirmed = true
                    };

                    userManager.CreateAsync(user1, "Abc!98765");
                }

                if (!context.Roles.Any())
                {

                    context.Roles.AddRange(

                            new IdentityRole { Id = "Logopedist", Name = "Logopedist", NormalizedName = "logopedist" },
                            new IdentityRole { Id = "Parents", Name = "Parents", NormalizedName = "parents" },
                            new IdentityRole { Id = "Mutualiteit", Name = "Mutualiteit", NormalizedName = "mutualiteit" },
                              new IdentityRole { Id = "Admin", Name = "Admin", NormalizedName = "admin" }

                            );

                    context.SaveChanges();
                }



                if (user1 != null)
                {
                    context.UserRoles.AddRange(

                        new IdentityUserRole<string> { UserId = user1.Id, RoleId = "Admin" }
                        //new IdentityUserRole<string> { UserId = user.Id, RoleId = "User" }

                        );

                    context.SaveChanges();
                }

            }



        }
    }
}
