using Shared.Models;

namespace Shared.Dto
{
    public class AuthResponse
    {
        public User user { get; set; }
        public string Token { get; set; }
        
        public AuthResponse( User user, string token)
        {
            this.user = user;
            this.Token = token;
        }
    }
}
