using Microsoft.AspNetCore.Mvc;
using UserManagement.API.Dtos;

namespace UserManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        [HttpPost("login")]
        public IActionResult Login(LoginDto dto)
        {
            if (dto.Username == "admin" && dto.Password == "1234")
            {
                return Ok(new { success = true, message = "Login successful" });
            }
            return Unauthorized(new { success = false, message = "Invalid credentials" });
        }
    }
}
