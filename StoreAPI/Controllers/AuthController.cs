using Microsoft.AspNetCore.Mvc;
using Shared.Dto;
using Service.Services;
using Shared.Models;
using System.Net;


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

            try
            {
                var response = await _authService.SignIn(loginDto);
                AppendRefreshTokenCookie(response.user, HttpContext.Response.Cookies);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

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

        private static void AppendRefreshTokenCookie(User user, IResponseCookies cookies)
        {
            var options = new CookieOptions();
            options.HttpOnly = true;
            //options.Secure = true;
            options.SameSite = SameSiteMode.Strict;
            options.Expires = DateTime.Now.AddMinutes(60);
            cookies.Append("RefreshTokenCookieKey", user.SecurityStamp, options);
        }

        [HttpPost("refresh-token")]
        public async Task<ActionResult<AuthResponse>> RefreshToken()
        {
            try
            {
                var refreshToken = Request.Cookies["RefreshTokenCookieKey"];
                if (string.IsNullOrEmpty(refreshToken))
                {
                    return Unauthorized();
                }

                var authResponse = await _authService.RefreshToken(refreshToken);

                return Ok(authResponse);


            }
            catch (Exception ex)
            {
                return Unauthorized();
            }
        }
    }
}