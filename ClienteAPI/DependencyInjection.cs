using ClienteAPI.Contracts;
using ClienteAPI.Models;
using ClienteAPI.Repositories;
using ClienteAPI.Services;
using ClienteAPI.Validators;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace ClienteAPI;

public static class DependencyInjection
{
    public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IClienteRepository, ClienteRepository>();
        services.AddScoped<IClienteService, ClienteService>();
        services.AddTransient<IValidator<ClienteCreateRequest>, ClienteCreateRequestValidator>();
        services.AddTransient<IValidator<ClienteUpdateRequest>, ClienteUpdateRequestValidator>();

        services.AddDbContext<ClienteDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DatabaseConnection")));

        services.AddAutoMapper(typeof(AppMappingProfile));

        return services;
    }
}
