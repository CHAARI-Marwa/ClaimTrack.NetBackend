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
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            var users = await userRepository.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await userRepository.GetUserByIdAsync(id);
            if (user == null) return NotFound();

            return Ok(user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, User user)
        {
            if (id != user.Id)
                return BadRequest("User ID mismatch.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingUser = await userRepository.GetUserByIdAsync(id);
            if (existingUser == null)
                return NotFound();

            existingUser.FirstName = user.FirstName;
            existingUser.LastName = user.LastName;
            existingUser.Email = user.Email;
            existingUser.Password = user.Password;
            existingUser.Role = user.Role;

            await userRepository.UpdateUserAsync(existingUser);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var deleted = await userRepository.DeleteUserAsync(id);
            if (!deleted) return NotFound();

            return NoContent();
        }

    }
}
