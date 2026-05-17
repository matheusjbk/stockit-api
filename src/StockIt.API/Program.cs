using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;
using StockIt.Application.DependencyInjection;
using StockIt.Domain.ValueObjects;
using StockIt.Infra.Data;
using StockIt.Infra.DependencyInjection;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddRouting(options => options.LowercaseUrls = true);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetValue<string>("Settings:Jwt:SecretKey")!)),
        RoleClaimType = ClaimTypes.Role
    };
});
builder.Services.AddAuthorization();

builder.Services.AddHttpContextAccessor();

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

await SeedRoles();

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

async Task SeedRoles()
{
    using var scope = app.Services.CreateScope();
    var services = scope.ServiceProvider;

    var roleManager = services.GetRequiredService<RoleManager<IdentityRole<Guid>>>();

    var roles = Role.GetAll();

    foreach(var role in roles)
    {
        var roleExist = await roleManager.RoleExistsAsync(role.Name);

        if (!roleExist) await roleManager.CreateAsync(new IdentityRole<Guid>(role.Name));
    }
}