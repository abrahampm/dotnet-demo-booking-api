using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using alten_test.Core.Dto.Authentication;
using alten_test.Core.Models.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace alten_test.PresentationLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;  
        private readonly RoleManager<IdentityRole> _roleManager;  
        private readonly IConfiguration _configuration;  
  
        public AuthController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)  
        {  
            _userManager = userManager;  
            _roleManager = roleManager;  
            _configuration = configuration;
        }
        
        [HttpPost]  
        [Route("login")]  
        public async Task<IActionResult> Login([FromBody] AuthLoginDto model)  
        {  
            var user = await _userManager.FindByEmailAsync(model.Email);  
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))  
            {  
                var userRoles = await _userManager.GetRolesAsync(user);  
  
                var authClaims = new List<Claim>  
                {  
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),  
                };  
  
                foreach (var userRole in userRoles)  
                {  
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));  
                }  
  
                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));  
  
                var token = new JwtSecurityToken(  
                    issuer: _configuration["JWT:ValidIssuer"],  
                    audience: _configuration["JWT:ValidAudience"],  
                    expires: DateTime.Now.AddHours(3),  
                    claims: authClaims,  
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)  
                    );  
  
                return Ok(new  
                {  
                    token = new JwtSecurityTokenHandler().WriteToken(token),  
                    expiration = token.ValidTo  
                });  
            }  
            return Unauthorized();  
        }  
  
        [HttpPost]  
        [Route("register")]  
        public async Task<IActionResult> Register([FromBody] AuthRegisterDto model)  
        {  
            var userExists = await _userManager.FindByEmailAsync(model.Email);  
            if (userExists != null)  
                return StatusCode(StatusCodes.Status409Conflict, new AuthResponseDto { Status = "Error", Message = "User already exists!" });  
  
            ApplicationUser user = new ApplicationUser()  
            {  
                Email = model.Email,
                UserName =  Regex.Replace(model.FirstName, "[^a-zA-Z0-9_.]+", "", RegexOptions.Compiled) + "." + Regex.Replace(model.LastName, "[^a-zA-Z0-9_.]+", "", RegexOptions.Compiled),
                FirstName = model.FirstName,
                LastName = model.LastName,
                BirthDate = model.BirthDate,
                SecurityStamp = Guid.NewGuid().ToString()  
            };  
            var result = await _userManager.CreateAsync(user, model.Password);  
            if (!result.Succeeded)  
                return StatusCode(StatusCodes.Status500InternalServerError, new AuthResponseDto { Status = "Error", Message = "User creation failed! Please check user details and try again." });

            if (!await _roleManager.RoleExistsAsync(ApplicationUserRoles.User))
            {
                await _roleManager.CreateAsync(new IdentityRole(ApplicationUserRoles.User));
            }
            
            if (await _roleManager.RoleExistsAsync(ApplicationUserRoles.User))  
            {  
                await _userManager.AddToRoleAsync(user, ApplicationUserRoles.User);  
            } 

            return Ok(new AuthResponseDto { Status = "Success", Message = "User created successfully!" });  
        }  
  
        [HttpPost]  
        [Route("register-admin")]  
        public async Task<IActionResult> RegisterAdmin([FromBody] AuthRegisterDto model)  
        {  
            var userExists = await _userManager.FindByEmailAsync(model.Email);  
            if (userExists != null)  
                return StatusCode(StatusCodes.Status500InternalServerError, new AuthResponseDto { Status = "Error", Message = "User already exists!" });  
  
            ApplicationUser user = new ApplicationUser()  
            {  
                Email = model.Email,
                UserName =  Regex.Replace(model.FirstName, "[^a-zA-Z0-9_.]+", "", RegexOptions.Compiled) + "." + Regex.Replace(model.LastName, "[^a-zA-Z0-9_.]+", "", RegexOptions.Compiled),  
                FirstName = model.FirstName,
                LastName = model.LastName,
                BirthDate = model.BirthDate,
                SecurityStamp = Guid.NewGuid().ToString()
            };  
            var result = await _userManager.CreateAsync(user, model.Password);  
            if (!result.Succeeded)  
                return StatusCode(StatusCodes.Status500InternalServerError, new AuthResponseDto { Status = "Error", Message = "User creation failed! Please check user details and try again." });

            if (!await _roleManager.RoleExistsAsync(ApplicationUserRoles.Admin))
            {
                await _roleManager.CreateAsync(new IdentityRole(ApplicationUserRoles.Admin));
            }

            if (await _roleManager.RoleExistsAsync(ApplicationUserRoles.Admin))  
            {  
                await _userManager.AddToRoleAsync(user, ApplicationUserRoles.Admin);  
            }  
  
            return Ok(new AuthResponseDto { Status = "Success", Message = "Admin created successfully!" });  
        }  
    }
}