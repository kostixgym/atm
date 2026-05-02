using Lab5.Application.ServiceCollectionExtensions;
using Lab5.Infrastructure.ServiceCollectionExtensions;
using Lab5.Presentation.Controllers;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

string systemPassword = builder.Configuration["SystemPassword"] ?? "admin123";

builder.Services.AddControllers().AddApplicationPart(typeof(SessionController).Assembly)
    .AddApplicationPart(typeof(AccountController).Assembly);

builder.Services.AddInfrastructure();
builder.Services.AddApplication(systemPassword);

WebApplication app = builder.Build();

app.MapControllers();

app.Run();