using Microsoft.Extensions.DependencyInjection;

namespace Auth
{
    public static class AuthExtension
    {
        public static void AddAuthenticationLayer(this IServiceCollection services)
        {
            //    services.AddAuthentication(options =>
            //    {
            //        options.DefaultAuthenticateScheme = "Bearer";
            //        options.DefaultChallengeScheme = "Bearer";
            //    })
            //    .AddJwtBearer("Bearer", options =>
            //    {
            //        options.Authority = "https://localhost:5001";
            //        options.Audience = "StoreAPI";
            //    });

            //}
        }
    }
}
