using UserRepository.DBContexts;
using UserRepository.Repositories;
using Microsoft.EntityFrameworkCore;

namespace UserRepository.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCustomServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connString = configuration.GetConnectionString("DefaultConnection");
        var allowedOrigins = configuration.GetSection("Cors:AllowedOrigins").Get<string[]>();

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddDbContext<UsersDbContext>(options =>
            options.UseNpgsql(connString));

        services.AddScoped<IUserRepository, UsersRepository>();

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
