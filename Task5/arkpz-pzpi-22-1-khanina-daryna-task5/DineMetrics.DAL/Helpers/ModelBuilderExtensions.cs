using DineMetrics.Core.Enums;
using DineMetrics.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace DineMetrics.DAL.Helpers;

public static class ModelBuilderExtensions
{
    public static void Seed(this ModelBuilder modelBuilder)
    {
        var admin = new User()
        {
            Id = 1,
            Email = "admin@gmail.com",
            PasswordHash = "f9c355b602a10ee3e31c2f2c23acdcba3b299ddcf9607ba0d10ae9d041e8e09b",
            Role = UserRole.Admin,
            AppointmentDate = new DateOnly(2022, 11, 28)
        };
        
        modelBuilder.Entity<User>().HasData(admin);
    }
}