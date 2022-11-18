using Parking.API.Models;

namespace Parking.API.Repositories.Interfaces
{
    public interface IReservationRepository
    {
        Reservation Get(int id);
        List<Reservation> GetAll();
        void Add(Reservation item);
        void Update(Reservation reservation);
        public bool Save();
    }
}
