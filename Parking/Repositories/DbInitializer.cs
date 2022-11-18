using Parking.Data;
using Parking.Models;
using Parking.Repositories.Interfaces;

namespace Parking.Repositories
{
    public class DbInitializer : IDbInitializer
    {
        private readonly ApplicationDbContext applicationDbContext;

        public DbInitializer(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        public void Initialize()
        {
            if (!applicationDbContext.ParkingSpots.Any())
            {
                for (int i = 0; i < 170; i++)
                {
                    var parkingSpot = new ParkingSpot
                    {
                        IsAvailable = true,
                    };

                    applicationDbContext.ParkingSpots.Add(parkingSpot);
                    applicationDbContext.SaveChanges();
                }
            }
        }
    }
}
