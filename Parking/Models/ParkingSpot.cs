using System.ComponentModel.DataAnnotations;

namespace Parking.API.Models
{
    public class ParkingSpot
    {
        [Key]
        public int ParkingSpotId { get; set; }

        [Required]
        public bool IsAvailable { get; set; }
    }
}