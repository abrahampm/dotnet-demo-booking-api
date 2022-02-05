using System;
using System.ComponentModel.DataAnnotations;

namespace alten_test.Core.Dto
{
    public class ReservationDtoInput
    {   
        public int Id { get; set; }

        [Required]
        public DateTime StartDate { get; set; }
        
        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public ContactDtoInput Contact { get; set; }

        [Required]
        public RoomDtoInput Room { get; set; }
        
        public string Description { get; set; }
    }
}