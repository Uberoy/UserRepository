using CardRepository.Entities;
using CardRepository.Repositories;

namespace CardRepository.Endpoints
{
    public static class EndpointMapping
    {
        public static WebApplication MapEndpoints(this WebApplication app)
        {
            app.MapGet("/cards", async (ICardRepository repo) =>
            {
                return await repo.GetAllAsync();
            });

            app.MapPost("/cards", async (ICardRepository repo, Card card) =>
            {
                await repo.AddAsync(card);
            });

            return app;
        }
    }
}
