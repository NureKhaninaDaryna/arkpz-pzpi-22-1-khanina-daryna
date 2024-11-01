using DineMetrics.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace DineMetrics.DAL.Helpers;

public static class ModelBuilderExtensions
{
    public static void Seed(this ModelBuilder modelBuilder)
    {
        // for adding seed data
    }

    public static void Configure(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasOne(u => u.Admin)
            .WithOne(a => a.User)
            .HasForeignKey<Admin>(a => a.Id);
        
        modelBuilder.Entity<User>()
            .HasOne(u => u.Manager)
            .WithOne(m => m.User)
            .HasForeignKey<Manager>(m => m.Id);
    }
}