using CuentaAPI.Contracts;
using CuentaAPI.Models;
using CuentaAPI.Repositories;
using CuentaAPI.Services;
using CuentaAPI.Validators;
using FluentValidation;
using MassTransit;
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
        services.AddTransient<IClienteService, ClienteService>();

        services.AddDbContext<CuentaDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DatabaseConnection")));

        services.AddAutoMapper(typeof(AppMappingProfile));

        services.ConfigureRabbitMqServices(configuration);

        return services;
    }

    public static IServiceCollection ConfigureRabbitMqServices(this IServiceCollection services, IConfiguration configuration)
    {
        var rabbitMQSettings = configuration.GetSection("RabbitMQ");
        services.AddMassTransit(x =>
        {
            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(rabbitMQSettings["Host"], c => {
                    c.Username(rabbitMQSettings["Username"]);
                    c.Password(rabbitMQSettings["Password"]);
                });
                cfg.ConfigureEndpoints(context);
            });
        });

        return services;
    }
}
