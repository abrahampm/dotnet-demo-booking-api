using System.ComponentModel.DataAnnotations;

namespace alten_test.Core.Dto.Authentication
{
    public class AuthLoginDto
    {
        [EmailAddress]  
        [Required(ErrorMessage = "Email is required")]  
        public string Email { get; set; }  
  
        [Required(ErrorMessage = "Password is required")]  
        public string Password { get; set; }  
    }
}