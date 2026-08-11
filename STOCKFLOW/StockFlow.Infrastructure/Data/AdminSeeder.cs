using StockFlow.Application.Interfaces;
using StockFlow.Domain.Entities;
using StockFlow.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockFlow.Infrastructure.Data
{
    public static class AdminSeeder
    {
        public static async Task SeedAsync(
            ApplicationDbContext context,
            IPasswordHasher passwordHasher)
        {
            const string adminEmail = "admin@stockflow.com";

            var existingUser = await context.Users
                .FirstOrDefaultAsync(u => u.Email == adminEmail);

            if (existingUser is not null)
            {
                if (existingUser.Role != UserRole.Admin)
                {
                    existingUser.Role = UserRole.Admin;
                    existingUser.UpdatedAt = DateTime.UtcNow;

                    await context.SaveChangesAsync();
                }

                return;
            }

            var admin = new User
            {
                FullName = "StockFlow Admin",
                Email = adminEmail,
                PasswordHash = passwordHasher.HashPassword("Admin@12345"),
                Role = UserRole.Admin,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            await context.Users.AddAsync(admin);

            await context.SaveChangesAsync();
        }
    }
}
