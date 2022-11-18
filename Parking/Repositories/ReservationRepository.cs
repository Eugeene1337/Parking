using Parking.Data;
using Parking.Models;
using Parking.Repositories.Interfaces;

namespace Parking.Repositories
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly ApplicationDbContext _context;

        public ReservationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(Reservation item)
        {
            _context.Reservations.Add(item);
        }

        public void Update(Reservation reservation)
        {
            _context.Reservations.Update(reservation);
        }

        public Reservation Get(int id)
        {
            return _context.Reservations.Find(id);
        }

        public List<Reservation> GetAll()
        {
            return _context.Reservations.ToList();
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
