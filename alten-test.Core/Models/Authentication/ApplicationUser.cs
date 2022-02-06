using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace alten_test.Core.Models.Authentication
{
    public class ApplicationUser : IdentityUser
    {
        
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }
        
        [Required]
        public DateTime BirthDate { get; set; }
        
        public List<Reservation> Reservations { get; set; }
    }
}