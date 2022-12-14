using Parking.API.Data;
using Parking.API.Models;
using Parking.API.Repositories.Interfaces;

namespace Parking.API.Repositories
{
    public class ParkingSpotRepository : IParkingSpotRepository
    {
        private readonly ApplicationDbContext _context;

        public ParkingSpotRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(ParkingSpot item)
        {
            _context.ParkingSpots.Add(item);
        }

        public ParkingSpot Get(int id)
        {
            return _context.ParkingSpots.Find(id);
        }

        public List<ParkingSpot> GetAll()
        {
            return _context.ParkingSpots.ToList();
        }

        public void Update(ParkingSpot item)
        {
            _context.ParkingSpots.Update(item);
        }

        public bool Save()
        {
            return _context.SaveChanges() >= 0;
        }
    }
}
