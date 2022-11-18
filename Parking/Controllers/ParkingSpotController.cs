using Microsoft.AspNetCore.Mvc;
using Parking.Models;
using Parking.Repositories.Interfaces;

namespace Parking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParkingSpotController : ControllerBase
    {
        private readonly IParkingSpotRepository parkingSpotRepository;

        public ParkingSpotController(IParkingSpotRepository parkingSpotRepository)
        {
            this.parkingSpotRepository = parkingSpotRepository;
        }

        [HttpGet]
        [Route("GetAll")]
        public ActionResult<IEnumerable<ParkingSpot>> GetAll()
        {
            return parkingSpotRepository.GetAll();
        }

        [HttpGet]
        [Route("Get{id}")]
        public ActionResult<ParkingSpot> Get(int id)
        {
            var parkingSpot = parkingSpotRepository.Get(id);

            if (parkingSpot == null)
            {
                return NotFound();
            }

            return parkingSpot;
        }

        [HttpPost]
        [Route("Add")]
        public IActionResult Add()
        {
            parkingSpotRepository.Add(new ParkingSpot { IsAvailable = true});

            if (!parkingSpotRepository.Save())
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Status = "Error", Message = "Add a parking spot failed on save." });
            }

            var spot = parkingSpotRepository.GetAll().Last();
            return Ok(new
            {
                Message = $"New parking spot added. ParkingSpotId [{spot.ParkingSpotId}], IsAvailable: {spot.IsAvailable}",
            });
        }
    }
}
