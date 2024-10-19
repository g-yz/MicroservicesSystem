using ClienteAPI.Consumers;
using ClienteAPI.Contracts;
using ClienteAPI.Events;
using ClienteAPI.Models;
using ClienteAPI.Repositories;
using ClienteAPI.Services;
using ClienteAPI.Validators;
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

        services.AddAutoMapper(typeof(AppMappingProfile));

        services.ConfigureRabbitMqServices(configuration);

        return services;
    }

    public static IServiceCollection ConfigureRabbitMqServices(this IServiceCollection services, IConfiguration configuration)
    {
        var rabbitMQSettings = configuration.GetSection("RabbitMQ");
        services.AddMassTransit(x =>
        {
            x.AddConsumer<VerificarClienteCommandConsumer>();

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
