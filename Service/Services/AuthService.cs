using AutoMapper;
using Service.Exceptions;
using Repository.Repositories;
using Shared.Dto;
using Shared.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Service.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtService _jwtService;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;


        public AuthService(IUserRepository userRepository, IJwtService jwtService, IMapper mapper, UserManager<User> userManager)
        {
            _userManager = userManager;
            _userRepository = userRepository;
            _jwtService = jwtService;
            _mapper = mapper;
        }

        public async Task<AuthResponse> RefreshToken(string refreshToken)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(user => user.SecurityStamp == refreshToken);
            var roles = await _userManager.GetRolesAsync(user);
            if (user != null)
            {
                var jwtToken = _jwtService.GenerateToken(user,roles);
                return new AuthResponse(user, jwtToken);
            }
            else
            {
                throw new UnauthorizedAccessException("Invalid refresh token");
            }
        }

        public async Task<AuthResponse?> SignIn(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null)
            {
                throw new Exception("User not found");
            }

           

            var result = await _userManager.CheckPasswordAsync(user, loginDto.Password);
            if (!result)
            {
                throw new Exception("Invalid email or password");
            }
            
            var roles = await _userManager.GetRolesAsync(user);

            var response = new AuthResponse(user, _jwtService.GenerateToken(user, roles));


            return response;

        }

        public async Task<User?> SignUp(RegisterDto registerDto)
        {

            var user = new User()
            {
                Email = registerDto.Email,
                FullName = registerDto.FullName,
                EmailConfirmed = true,
                UserName = registerDto.Email,
            };
           
            var result = await _userManager.CreateAsync(user, registerDto.Password);
            await _userManager.AddToRoleAsync(user, "User");
            
            if (result.Succeeded)
            {
                return user;
            }
            else
            {
            throw new Exception("User not created");
            }
        }

    }
}
