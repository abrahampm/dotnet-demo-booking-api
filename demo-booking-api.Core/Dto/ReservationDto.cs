using System;
using System.ComponentModel.DataAnnotations;
using alten_test.Core.Dto.Authentication;
using alten_test.Core.Models.Authentication;

namespace alten_test.Core.Dto
{
    public class ReservationDto
    {
        [Required]
        public int Id { get; set; }
        
        [Required]
        public DateTime StartDate { get; set; }
        
        [Required]
        public DateTime EndDate { get; set; }
        
        [Required]
        public RoomDto Room { get; set; }
        
        public ApplicationUserDto ApplicationUser { get; set; }

        public string Description { get; set; }

    }
}