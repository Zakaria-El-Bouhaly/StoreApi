using AutoMapper;
using Service.Services;
using Repository.Exceptions;
using Repository.Repositories;
using Shared.Dto;
using Shared.Models;

namespace Service.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtService _jwtService;
        private readonly IMapper _mapper;


        public AuthService(IUserRepository userRepository, IJwtService jwtService, IMapper mapper)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
            _mapper = mapper;
        }   


        public async Task<AuthResponse?> SignIn(LoginDto loginDto)
        {
            var user = await _userRepository.GetUserByEmail(loginDto.Email);

            if (user != null && BCrypt.Net.BCrypt.Verify(loginDto.Password, user.Password))
            {
                var token = _jwtService.generateToken(user);
                return new AuthResponse(user, token);
            }

            return null;

        }

        public async Task<User?> SignUp(RegisterDto registerDto)
        {

            var existingUser = await _userRepository.GetUserByEmail(registerDto.Email);

            if (existingUser != null)
            {
                throw new UserAlreadyExistsException("User already exists");
            }

           User user = _mapper.Map<User>(registerDto);

            
            user.Password = BCrypt.Net.BCrypt.HashPassword(registerDto.Password);

            return await _userRepository.AddUser(user);            
        }
    }
}
