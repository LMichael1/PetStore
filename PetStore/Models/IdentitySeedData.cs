﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace PetStore.Models
{
    public static class IdentitySeedData
    {
        private const string adminUser = "Admin";
        private const string adminPassword = "Secret123$";

        public static async Task EnsurePopulated(IApplicationBuilder app)
        {
            UserManager<ApplicationUser> userManager = app.ApplicationServices
                .GetRequiredService<UserManager<ApplicationUser>>();

            ApplicationUser user = await userManager.FindByIdAsync(adminUser);
            if (user == null)
            {
                user = new ApplicationUser("Admin");
                await userManager.CreateAsync(user, adminPassword);
            }
        }
    }
}
