using System;
using System.Linq;
using PostSystemAPI.Common.Enums;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PostSystemAPI.DAL.Context;
using PostSystemAPI.DAL.Models;

namespace PostSystemAPI.Extensions;

public static class DatabaseExtensions
{
    public static void ApplyMigrations(this IApplicationBuilder app)
    {
        using (var scope = app.ApplicationServices.CreateScope())
        {
            using var context = scope.ServiceProvider.GetRequiredService<PostSystemContext>();
            context.Database.Migrate();
        }
    }

    public static void SeedDatabase(this IApplicationBuilder app, IConfiguration configuration)
    {
        using var scope = app.ApplicationServices.CreateScope();
        using var context = scope.ServiceProvider.GetRequiredService<PostSystemContext>();
        using var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
        if (!userManager.GetUsersInRoleAsync(Roles.Administrator.ToString()).GetAwaiter().GetResult().Any())
        {
            var adminUser = new User()  
            {
                Email = configuration["DefaultUser:Email"],
                UserName = $"{configuration["DefaultUser:FirstName"]}_{configuration["DefaultUser:LastName"]}",
                FirstName = configuration["DefaultUser:FirstName"],
                LastName = configuration["DefaultUser:LastName"], Balance = 100000
            };
            var isCreated = userManager.CreateAsync(adminUser,
                    configuration["DefaultUser:Password"] ?? throw new InvalidOperationException())
                .GetAwaiter()
                .GetResult();
            if (isCreated.Succeeded)
            {
                userManager.AddToRoleAsync(adminUser, Roles.Administrator.ToString()).GetAwaiter().GetResult();
            }
            else
            {
                throw new InvalidOperationException();
            }
        }
    }
    
}