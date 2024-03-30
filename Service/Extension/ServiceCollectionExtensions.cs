using Service.Services;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using Service.Services;

namespace Service.Extension
{

    public static class ServiceCollectionExtension
    {
        

        public static void AddServiceLayer(this IServiceCollection services)
        {
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IJwtService, JwtService>();

            var mappingConfig = new MapperConfiguration(mc => { mc.AddProfile(new MapperConfig()); });
            services.AddAutoMapper(typeof(MapperConfig));
            services.AddScoped<IJwtService, JwtService>();
        }
    }


}