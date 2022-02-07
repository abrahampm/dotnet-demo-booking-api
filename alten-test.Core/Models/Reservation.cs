using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        [ForeignKey("ApplicationUser")]
        public string ApplicationUserId { get; set; }

        [Required]
        public virtual ApplicationUser ApplicationUser { get; set; }

        [Required]
        [ForeignKey("Room")]
        public int RoomId { get; set; }
        
        [Required]
        public virtual Room Room { get; set; }

        public string Description { get; set; }

    }
}