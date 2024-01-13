using System;
using System.ComponentModel.DataAnnotations;

namespace autoryzacja.Models  // Replace with your actual namespace
{
    public class CarReservation
    {
        public int Id { get; set; }

        [Required]
        public int CarId { get; set; }  // Assuming CarId is a foreign key to a Car entity

        [Required]
        public DateTime PickupDate { get; set; }

        [Required]
        public DateTime ReturnDate { get; set; }

        // Add other fields as necessary, for example, UserId if reservations are user-specific

        // You can also include navigation properties to related entities, e.g., Car and User
        // public virtual Car Car { get; set; }
        // public virtual ApplicationUser User { get; set; }
    }
}
