using Shared.Models;

namespace Service.Services;
public interface IJwtService
{ 
    public string GenerateToken(User user,IList<string> roles);
}