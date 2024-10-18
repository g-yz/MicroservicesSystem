using ClienteAPI.Models;
using ClienteAPI.Repositories;
using ClienteAPI.Services;
using Microsoft.EntityFrameworkCore;

namespace ClienteAPI;

public static class DependencyInjection
{
    public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IClienteRepository, ClienteRepository>();
        services.AddScoped<IClienteService, ClienteService>();

        services.AddDbContext<ClienteDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        services.AddAutoMapper(typeof(AppMappingProfile));

        return services;
    }
}
