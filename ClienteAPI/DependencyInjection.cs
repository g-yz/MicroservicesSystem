using ClienteAPI.Models;
using ClienteApp.Data.Repositories;
using ClienteApp.Messaging.Mappers;
using ClienteApp.Messaging.Producers;
using ClienteApp.Services.Contracts;
using ClienteApp.Services.Mappers;
using ClienteApp.Services.Services;
using ClienteApp.Services.Validators;
using FluentValidation;
using MassTransit;
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
        services.AddTransient<IClienteEventPublisher, ClienteEventPublisher>();

        services.AddDbContext<ClienteDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DatabaseConnection")));

        services.AddAutoMapper(typeof(ServicesProfile));
        services.AddAutoMapper(typeof(MessaggingProfile));

        services.ConfigureMessagingServices(configuration);

        return services;
    }

    public static IServiceCollection ConfigureMessagingServices(this IServiceCollection services, IConfiguration configuration)
    {
        var rabbitMQSettings = configuration.GetSection("RabbitMQ");
        services.AddMassTransit(x =>
        {
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
