using BackendDotNet.Api.Endpoints;
using BackendDotNet.Application;
using BackendDotNet.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

var allowedOrigins = builder.Configuration
    .GetSection("Cors:AllowedOrigins")
    .Get<string[]>() ?? Array.Empty<string>();

builder.Services.AddCors(options =>
    options.AddPolicy("Portfolio", policy => policy
        .WithOrigins(allowedOrigins)
        .AllowAnyHeader()
        .AllowAnyMethod()));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseHttpsRedirection();  
}

app.UseCors("Portfolio");

app.MapContactEndpoints();
app.MapGet("/health", () => Results.Ok(new { status = "ok" }));

app.Run();