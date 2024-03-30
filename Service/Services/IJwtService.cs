using Shared.Models;

namespace Service.Services;
public interface IJwtService
{ 
    public string generateToken(User user);
}