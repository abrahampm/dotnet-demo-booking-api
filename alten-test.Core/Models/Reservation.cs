using System;
using System.ComponentModel.DataAnnotations;
using alten_test.Core.Models.Authentication;

namespace alten_test.Core.Models
{
    public class Reservation : BaseEntity
    {
        [Required]
        public DateTime StartDate { get; set; }
        
        [Required]
        public DateTime EndDate { get; set; }
        
        [Required]
        public string ApplicationUserId { get; set; }

        [Required]
        public ApplicationUser ApplicationUser { get; set; }

        [Required]
        public int RoomId { get; set; }
        
        [Required]
        public Room Room { get; set; }

        public string Description { get; set; }

    }
}