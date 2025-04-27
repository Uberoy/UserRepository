using Microsoft.EntityFrameworkCore;

namespace UserRepository.Extensions
{
    public static class DatabaseMigrationExtensions
    {
        public static void ApplyMigrations<TContext>(this IHost app) where TContext : DbContext
        {
            using var scope = app.Services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<TContext>();
            db.Database.Migrate();
        }
    }
}