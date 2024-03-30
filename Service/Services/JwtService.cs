using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Shared.Models;

namespace Service.Services;

public class JwtService : IJwtService
{

    private readonly IConfiguration _configuration;


    public JwtService(IConfiguration configuration)
    {

        _configuration = configuration;
    }



    public string generateToken(User user)
    {
        var role = user.IsAdmin ? "Admin" : "User";

        var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, EpochTime.GetIntDate(DateTime.Now).ToString(CultureInfo.InvariantCulture), ClaimValueTypes.Integer64),
                        new Claim("id", user.Id.ToString()),
                        new Claim(ClaimTypes.Role, role)
                    };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            _configuration["Jwt:Issuer"],
            _configuration["Jwt:Audience"],
            claims,
            expires: DateTime.UtcNow.AddMinutes(90),
            signingCredentials: signIn);

        // return token 
        return new JwtSecurityTokenHandler().WriteToken(token);
    }


}