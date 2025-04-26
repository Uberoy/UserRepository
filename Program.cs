using CardRepository.DBContexts;
using CardRepository.Entities;
using CardRepository.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Connection string
var connString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<CardsDbContext>(options =>
    options.UseNpgsql(connString));

// Add Repository
builder.Services.AddScoped<ICardRepository, CardsRepository>();

builder.Services
    .AddCors(options =>
        options.AddPolicy(
            "Default",
            policy =>
                policy
                    .WithOrigins("http://localhost:5000")
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials()
        )
    );

var app = builder.Build();

app.Urls.Add("http://0.0.0.0:5005");

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseCors("Default");

app.MapGet("/cards", async (ICardRepository repo) =>
{
    return await repo.GetAllAsync();
});
app.MapPost("/cards", async (ICardRepository repo, Card card) =>
{
    await repo.AddAsync(card);
});

app.Run();