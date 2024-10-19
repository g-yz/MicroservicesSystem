using CuentaAPI.Contracts;
using CuentaAPI.Models;
using CuentaAPI.Repositories;
using CuentaAPI.Services;
using CuentaAPI.Validators;
using FluentValidation;
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
        services.AddTransient<IValidator<CuentaCreateRequest>, CuentaCreateRequestValidator>();
        services.AddTransient<IValidator<CuentaUpdateRequest>, CuentaUpdateRequestValidator>();
        services.AddTransient<IValidator<MovimientoAddRequest>, MovimientoAddRequestValidator>();
        services.AddTransient<IValidator<MovimientoReporteFilter>, MovimientoReporteFilterValidator>();

        services.AddDbContext<CuentaDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DatabaseConnection")));

        services.AddAutoMapper(typeof(AppMappingProfile));

        return services;
    }
}
