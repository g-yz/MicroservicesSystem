using FluentValidation;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using AccountApp.Data.Repositories;
using AccountApp.Data.Models;
using AccountApp.Messaging.Consumers;
using AccountApp.Services.Services;
using AccountApp.Services.Contracts;
using AccountApp.Services.Mappers;
using AccountApp.Services.Validators;

namespace AccountApp.Api;

public static class DependencyInjection
{
    public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IAccountRepository, AccountRepository>();
        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<IMovementRepository, MovementRepository>();
        services.AddScoped<IMovementService, MovementService>();
        services.AddScoped<IClientRepository, ClientRepository>();
        services.AddTransient<IValidator<AccountCreateRequest>, AccountCreateRequestValidator>();
        services.AddTransient<IValidator<AccountUpdateRequest>, AccountUpdateRequestValidator>();
        services.AddTransient<IValidator<MovementAddRequest>, MovementAddRequestValidator>();
        services.AddTransient<IValidator<MovementReporteFilter>, MovementReporteFilterValidator>();

        services.AddDbContext<AccountDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DatabaseConnection")));

        services.AddAutoMapper(typeof(AppMappingProfile));

        services.ConfigureMessagingServices(configuration);

        return services;
    }

    public static IServiceCollection ConfigureMessagingServices(this IServiceCollection services, IConfiguration configuration)
    {
        var rabbitMQSettings = configuration.GetSection("RabbitMQ");
        services.AddMassTransit(x =>
        {
            x.AddConsumer<ClientCreatedEventConsumer>();
            x.AddConsumer<ClientDeletedEventConsumer>();
            x.AddConsumer<ClientUpdatedEventConsumer>();

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
