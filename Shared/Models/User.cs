using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;



namespace Shared.Models
{
    public class User : IdentityUser<int>
    {
        public string? FullName { get; set; }
        [JsonIgnore]
        public override string? PasswordHash { get; set; }
        [JsonIgnore]
        public override string? SecurityStamp { get; set; }
        [JsonIgnore]
        public override string? ConcurrencyStamp { get; set; }        
    }
}
