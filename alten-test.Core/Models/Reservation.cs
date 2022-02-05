using System;
using System.ComponentModel.DataAnnotations;

namespace alten_test.Core.Models
{
    public class Reservation : BaseEntity
    {
        [Required]
        public DateTime StartDate { get; set; }
        
        [Required]
        public DateTime EndDate { get; set; }
        
        [Required]
        public int ContactId { get; set; }

        [Required]
        public Contact Contact { get; set; }

        [Required]
        public int RoomId { get; set; }
        
        [Required]
        public Room Room { get; set; }

        public string Description { get; set; }

    }
}