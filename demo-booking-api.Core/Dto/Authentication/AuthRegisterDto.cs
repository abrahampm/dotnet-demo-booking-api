using System;
using System.ComponentModel.DataAnnotations;

namespace alten_test.Core.Dto.Authentication
{
    public class AuthRegisterDto
    {
        [EmailAddress]  
        [Required(ErrorMessage = "Email is required")]  
        public string Email { get; set; }  
        
        [Required(ErrorMessage = "Firstname is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Lastname is required")]
        public string LastName { get; set; }
        
        public DateTime BirthDate { get; set; }
  
        [Required(ErrorMessage = "Password is required")]  
        public string Password { get; set; }
    }
}