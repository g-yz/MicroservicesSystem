using ClientApp.Data.Models;
using ClientApp.Data.Repositories;
using ClientApp.Messaging.Mappers;
using ClientApp.Messaging.Producers;
using ClientApp.Services.Contracts;
using ClientApp.Services.Mappers;
using ClientApp.Services.Services;
using ClientApp.Services.Validators;
using FluentValidation;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace ClientApp.Api;

public static class DependencyInjection
{
    public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IClientRepository, ClientRepository>();
        services.AddScoped<IClientService, ClientService>();
        services.AddTransient<IValidator<ClientCreateRequest>, ClientCreateRequestValidator>();
        services.AddTransient<IValidator<ClientUpdateRequest>, ClientUpdateRequestValidator>();
        services.AddTransient<IClientEventPublisher, ClientEventPublisher>();

        services.AddDbContext<ClientDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DatabaseConnection")));

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
