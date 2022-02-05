using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace alten_test.Core.Models
{
    public class Contact : BaseEntity
    {
        [Required] 
        public string Email { get; set; }
        
        [Phone]
        public string Phone { get; set; }
        
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }
        
        [Required]
        public DateTime BirthDate { get; set; }
        
        public List<Reservation> Reservations { get; set; }

    }
}