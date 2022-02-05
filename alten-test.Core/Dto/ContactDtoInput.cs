using System;
using System.ComponentModel.DataAnnotations;

namespace alten_test.Core.Dto
{
    public class ContactDtoInput
    {
        public int Id { get; set; }

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
    }
}