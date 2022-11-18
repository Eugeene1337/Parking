using Parking.Models;

namespace Parking.Repositories.Interfaces
{
    public interface IParkingSpotRepository
    {
        ParkingSpot Get(int id);
        List<ParkingSpot> GetAll();
        void Add(ParkingSpot item);
        void Update(ParkingSpot item);
        public bool Save();
    }
}
