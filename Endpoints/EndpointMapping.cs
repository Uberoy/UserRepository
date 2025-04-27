using UserRepository.Entities;
using UserRepository.Repositories;

namespace UserRepository.Endpoints
{
    public static class EndpointMapping
    {
        public static WebApplication MapEndpoints(this WebApplication app)
        {
            app.MapGet("/users", async (IUserRepository repo) =>
            {
                return await repo.GetAllAsync();
            });

            app.MapPost("/users", async (IUserRepository repo, User user) =>
            {
                await repo.AddAsync(user);
            });

            return app;
        }
    }
}
