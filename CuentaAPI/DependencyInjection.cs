using FluentValidation;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using CuentaApp.Data.Repositories;
using CuentaApp.Data.Models;
using CuentaApp.Messaging.Consumers;
using CuentaApp.Services.Services;
using CuentaApp.Services.Contracts;
using CuentaApp.Services.Mappers;
using CuentaApp.Services.Validators;

namespace CuentaAPI;

public static class DependencyInjection
{
    public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ICuentaRepository, CuentaRepository>();
        services.AddScoped<ICuentaService, CuentaService>();
        services.AddScoped<IMovimientoRepository, MovimientoRepository>();
        services.AddScoped<IMovimientoService, MovimientoService>();
        services.AddScoped<IClienteRepository, ClienteRepository>();
        services.AddTransient<IValidator<CuentaCreateRequest>, CuentaCreateRequestValidator>();
        services.AddTransient<IValidator<CuentaUpdateRequest>, CuentaUpdateRequestValidator>();
        services.AddTransient<IValidator<MovimientoAddRequest>, MovimientoAddRequestValidator>();
        services.AddTransient<IValidator<MovimientoReporteFilter>, MovimientoReporteFilterValidator>();

        services.AddDbContext<CuentaDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DatabaseConnection")));

        services.AddAutoMapper(typeof(AppMappingProfile));

        services.ConfigureMessagingServices(configuration);

        return services;
    }

    public static IServiceCollection ConfigureMessagingServices(this IServiceCollection services, IConfiguration configuration)
    {
        var rabbitMQSettings = configuration.GetSection("RabbitMQ");
        services.AddMassTransit(x =>
        {
            x.AddConsumer<ClienteCreatedEventConsumer>();
            x.AddConsumer<ClienteDeletedEventConsumer>();
            x.AddConsumer<ClienteUpdatedEventConsumer>();

            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(rabbitMQSettings["Host"], c => {
                    c.Username(rabbitMQSettings["Username"]!);
                    c.Password(rabbitMQSettings["Password"]!);
                });
                cfg.ConfigureEndpoints(context);
            });
        });

        return services;
    }
}
