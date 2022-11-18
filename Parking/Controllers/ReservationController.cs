using Microsoft.AspNetCore.Mvc;
using Parking.API.Models;
using Parking.API.Models.DTO;
using Parking.API.Repositories.Interfaces;

namespace Parking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationRepository reservationRepository;
        private readonly IParkingSpotRepository parkingSpotRepository;

        public ReservationController(IReservationRepository reservationRepository, IParkingSpotRepository parkingSpotRepository)
        {
            this.reservationRepository = reservationRepository;
            this.parkingSpotRepository = parkingSpotRepository;
        }

        [HttpGet]
        [Route("GetAll")]
        public ActionResult<IEnumerable<Reservation>> GetAll()
        {
            return reservationRepository.GetAll();
        }

        [HttpGet]
        [Route("Get{id}")]
        public ActionResult<Reservation> Get(int id)
        {
            var reservation = reservationRepository.Get(id);

            if (reservation == null)
            {
                return NotFound();
            }

            return reservation;
        }

        [HttpPost]
        [Route("Start")]
        public IActionResult StartReservation([FromBody] AddReservation addReservation)
        {
            var reservation = new Reservation
            {
                ParkingSpotId = addReservation.ParkingSpotId,
                UserId = addReservation.UserId,
                StartDate = DateTime.Now,
            };

            reservationRepository.Add(reservation);

            if (!reservationRepository.Save())
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Status = "Error", Message = "Updating a reservation failed on save." });
            }

            var spot = parkingSpotRepository.Get(reservation.ParkingSpotId);
            spot.IsAvailable = false;
            parkingSpotRepository.Update(spot);

            if (!parkingSpotRepository.Save())
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Status = "Error", Message = "Updating a parking spot failed on save." });
            }

            var newReservation = reservationRepository.GetAll().Last();
            return Ok(new
            {
                Message = $"New reservation added. ReservationId: [{newReservation.ReservationId}], ParkingSpotId: [{newReservation.ParkingSpotId}], UserId: [{newReservation.UserId}], StartDate: [{newReservation.StartDate}]",
            });
        }

        [HttpPost]
        [Route("Close")]
        public IActionResult CloseReservation(int id)
        {
            var reservation = reservationRepository.Get(id);

            if (reservation == null)
            {
                return NotFound();
            }

            reservation.EndDate = DateTime.Now;

            reservationRepository.Update(reservation);

            if (!reservationRepository.Save())
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Status = "Error", Message = "Updating a reservation failed on save." });
            }

            var spot = parkingSpotRepository.Get(reservation.ParkingSpotId);
            spot.IsAvailable = true;
            parkingSpotRepository.Update(spot);

            if (!parkingSpotRepository.Save())
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Status = "Error", Message = "Updating a parking spot failed on save." });
            }

            return Ok(new
            {
                Message = $"Reservation closed successfully. ReservationId: [{reservation.ReservationId}], ParkingSpotId: [{reservation.ParkingSpotId}], UserId: [{reservation.UserId}], EndDate: [{reservation.EndDate}]",
            });
        }
    }
}
