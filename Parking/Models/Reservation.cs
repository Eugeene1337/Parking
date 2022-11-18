using System.ComponentModel.DataAnnotations;

namespace Parking.Models
{
    public class Reservation
    {
        [Key]
        public int ReservationId { get; set; }
        [Required]
        public int ParkingSpotId { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; } = null;
    }
}
