using ClaimTrack.NetBackend.Models;
using ClaimTrack.NetBackend.Repositories;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace ClaimTrack.NetBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository userRepository;

        public UserController(IUserRepository userRepository) 
        { 
            this.userRepository = userRepository;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var registeredUser = await userRepository.RegisterAsync(user);
                return CreatedAtAction(nameof(SignUp), new { id = registeredUser.Id }, registeredUser);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await userRepository.LoginAsync(loginRequest.Email, loginRequest.Password);

            if (user == null)
            {
                return Unauthorized(new { message = "Invalid email or password" });
            }

            // Retourner l'utilisateur ou un token JWT si besoin
            return Ok(new
            {
                message = "Login successful",
                user = new
                {
                    user.Id,
                    user.FirstName,
                    user.LastName,
                    user.Email,
                    user.Role
                }
            });
        }

    }
}
