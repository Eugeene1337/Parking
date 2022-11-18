using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Parking.API.Models;

namespace Parking.API.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ParkingSpot> ParkingSpots { get; set; }
        public DbSet<Reservation> Reservations { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder); // create tables for Identity

            builder.Entity<Reservation>()
               .HasOne(typeof(ParkingSpot))
               .WithMany()
               .HasForeignKey("ParkingSpotId");

            builder.Entity<Reservation>()
                .HasOne(typeof(IdentityUser))
                .WithMany()
                .HasForeignKey("UserId");
        }
    }
}
