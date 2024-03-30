using Microsoft.AspNetCore.Mvc;
using Shared.Dto;
using Service.Services;
using Shared.Models;


namespace StoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public IAuthService _authService { get; set; }

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthResponse>> Login([FromBody] LoginDto loginDto)
        {

            var response = await _authService.SignIn(loginDto);
            if (response == null)
            {
                return Unauthorized("Invalid email or password");
            }
            return Ok(response);

        }

        [HttpPost("register")]
        public async Task<ActionResult<User>> Register([FromBody] RegisterDto registerDto)
        {
            try
            {
                User createdUser = await _authService.SignUp(registerDto);
                return Ok(createdUser);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}