using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using StockIt.Application.DependencyInjection;
using StockIt.Infra.Data;
using StockIt.Infra.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddRouting(options => options.LowercaseUrls = true);

//builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

builder.Services.AddInfra(builder.Configuration);
builder.Services.AddApplication();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

MigrateDatabase();

await app.RunAsync();

void MigrateDatabase()
{
    using var scope = app.Services.CreateScope();
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<AppDbContext>();
    context.Database.Migrate();

    Console.WriteLine();
    Console.WriteLine("Database migrate successfully");
    Console.WriteLine();
}