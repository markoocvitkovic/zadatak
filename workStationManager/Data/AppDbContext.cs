using Microsoft.EntityFrameworkCore;
using System;
using workStationManager.Models;

namespace workStationManager.Data;

public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Role> Roles { get; set; } = null!;
    public DbSet<WorkPosition> WorkPositions { get; set; } = null!;
    public DbSet<UserWorkPosition> UserWorkPositions { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseNpgsql("Host=localhost;Port=5432;Username=postgres;Password=1234;Database=bb");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .ToTable("Users")
            .HasKey(u => u.Id);

        modelBuilder.Entity<User>()
            .HasOne(u => u.Role)
            .WithMany(r => r.Users)
            .HasForeignKey(u => u.RoleId)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<Role>()
            .ToTable("Roles")
            .HasKey(r => r.Id);

        modelBuilder.Entity<WorkPosition>()
            .ToTable("WorkPositions")
            .HasKey(wp => wp.Id);

        modelBuilder.Entity<UserWorkPosition>()
            .ToTable("UserWorkPositions")
            .HasKey(uwp => uwp.Id);

        modelBuilder.Entity<UserWorkPosition>()
            .HasOne(uwp => uwp.User)
            .WithMany(u => u.WorkPositions)
            .HasForeignKey(uwp => uwp.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<UserWorkPosition>()
            .HasOne(uwp => uwp.WorkPosition)
            .WithMany(wp => wp.Users)
            .HasForeignKey(uwp => uwp.WorkPositionId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<UserWorkPosition>()
        .Property(uwp => uwp.WorkDate)
        .HasConversion(
            v => v, 
            v => DateTime.SpecifyKind(v, DateTimeKind.Utc) 
        );
    }
}

