using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace alten_test.Core.Dto.Authentication
{
    public class ApplicationUserDto
    {   
        [Required]
        public string Id { get; set; }
        
        [Required]
        public string Email { get; set; }
        
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }
        
        [Required]
        public DateTime BirthDate { get; set; }
        
        public IList<string> Roles { get; set; }
    }
}