using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using GarageBlazor3.Models;

namespace GarageBlazor3.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<Vehicle> Vehicles { get; set; } = null!;
    public DbSet<VehicleType> VehicleTypes { get; set; } = null!;
    public DbSet<ParkingSpot> ParkingSpots { get; set; } = null!;
    public DbSet<ParkingSession> ParkingSessions { get; set; } = null!;
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Vehicle>()
            .HasOne(v => v.VehicleType)
            .WithMany(t => t.Vehicles)
            .HasForeignKey(v => v.VehicleTypeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Vehicle>()
            .HasOne(v => v.Owner)
            .WithMany()
            .HasForeignKey(v => v.OwnerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<ParkingSession>()
            .HasOne(p => p.Vehicle)
            .WithMany()
            .HasForeignKey(p => p.VehicleId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<ParkingSession>()
            .HasOne(p => p.ParkingSpot)
            .WithMany(s => s.ParkingSessions)
            .HasForeignKey(p => p.ParkingSpotId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.Entity<Vehicle>()
            .HasIndex(v => v.RegistrationNumber)
            .IsUnique();

        builder.Entity<VehicleType>()
            .HasIndex(t => t.Name)
            .IsUnique();

        builder.Entity<ParkingSpot>()
            .HasIndex(s => s.Number)
            .IsUnique();
    }
}