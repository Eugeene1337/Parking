using System.ComponentModel.DataAnnotations;

namespace Parking.API.Models.DTO
{
    public class AddReservation
    {
        [Required]
        public int ParkingSpotId { get; set; }
        [Required]
        public string UserId { get; set; }
    }
}
