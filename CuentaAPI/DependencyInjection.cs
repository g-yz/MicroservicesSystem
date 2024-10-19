using CuentaAPI.Models;
using CuentaAPI.Repositories;
using CuentaAPI.Services;
using Microsoft.EntityFrameworkCore;

namespace CuentaAPI;

public static class DependencyInjection
{
    public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ICuentaRepository, CuentaRepository>();
        services.AddScoped<ICuentaService, CuentaService>();
        services.AddScoped<IMovimientoRepository, MovimientoRepository>();
        services.AddScoped<IMovimientoService, MovimientoService>();

        services.AddDbContext<CuentaDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DatabaseConnection")));

        services.AddAutoMapper(typeof(AppMappingProfile));

        return services;
    }
}
