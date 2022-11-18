using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Parking.Models;

namespace Parking.Data
{
    public static class ModelBuilderExtension
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            var spots = new List<ParkingSpot>();

            for (int i = 1; i < 171; i++)
            {
                var ParkingSpot = new ParkingSpot
                {
                    ParkingSpotId = i,
                    IsAvailable = true,
                };

                spots.Add(ParkingSpot);
            }


            modelBuilder.Entity<ParkingSpot>().HasData(
            new ParkingSpot
            {
                ParkingSpotId = 1,
                IsAvailable = true,
            });
        }
    }
}
