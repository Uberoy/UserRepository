using CardRepository.DBContexts;
using CardRepository.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CardRepository.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCustomServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connString = configuration.GetConnectionString("DefaultConnection");
        var allowedOrigins = configuration.GetSection("Cors:AllowedOrigins").Get<string[]>();

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddDbContext<CardsDbContext>(options =>
            options.UseNpgsql(connString));

        services.AddScoped<ICardRepository, CardsRepository>();

        services.AddCors(options =>
        {
            options.AddPolicy(
                "Default",
                policy => policy
                    .WithOrigins(allowedOrigins)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials()
            );
        });

        return services;
    }
}
