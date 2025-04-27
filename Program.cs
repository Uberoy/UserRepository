using UserRepository.DBContexts;
using UserRepository.Endpoints;
using UserRepository.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Host (server) settings
builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.Configure(builder.Configuration.GetSection("Kestrel"));
});

// Register services
builder.Services.AddCustomServices(builder.Configuration);

var app = builder.Build();

app.ApplyMigrations<UsersDbContext>();

// Configure middleware
app.UseCustomMiddleware();
app.MapEndpoints();

app.Run();
