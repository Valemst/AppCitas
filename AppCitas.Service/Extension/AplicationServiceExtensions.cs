using AppCitas.Service.Data;
using AppCitas.Service.Interfaces;
using AppCitas.Service.Services;
using Microsoft.EntityFrameworkCore;

namespace AppCitas.Service.Extension;

public static class AplicationServiceExtensions
{
    public static IServiceCollection AddAplicationServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddScoped<ITokenService, TokenService>();
        services.AddDbContext<DataContext>(options =>
        {
            options.UseSqlite(config.GetConnectionString("DefaultConnection"));
        });
        
        return services;
    }

}
