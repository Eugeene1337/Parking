using System.ComponentModel.DataAnnotations;

namespace Parking.Models
{
    public class ParkingSpot
    {
        [Key]
        public int ParkingSpotId { get; set; }

        [Required]
        public bool IsAvailable { get; set; }
    }
}