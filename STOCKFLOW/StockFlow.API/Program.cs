using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StockFlow.Application.Interfaces;
using StockFlow.Application.Services;
using StockFlow.Infrastructure.Data;
using StockFlow.Infrastructure.Repositories;
using StockFlow.Infrastructure.Security;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// =====================================================
// Services
// =====================================================

builder.Services.AddControllers();


// =====================================================
// Database
// =====================================================

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));


// =====================================================
// Dependency Injection
// =====================================================

// Product
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();

// Category
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();

// Authentication
builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

// Unit of Work
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();


// =====================================================
// JWT Authentication
// =====================================================

var jwtKey = builder.Configuration["Jwt:Key"]
    ?? throw new InvalidOperationException(
        "JWT key is not configured.");

var jwtIssuer = builder.Configuration["Jwt:Issuer"]
    ?? throw new InvalidOperationException(
        "JWT issuer is not configured.");

var jwtAudience = builder.Configuration["Jwt:Audience"]
    ?? throw new InvalidOperationException(
        "JWT audience is not configured.");

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = jwtIssuer,
            ValidAudience = jwtAudience,

            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtKey))
        };
    });

builder.Services.AddAuthorization();


// =====================================================
// Build Application
// =====================================================

var app = builder.Build();


// =====================================================
// Seed Initial Admin
// =====================================================

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider
        .GetRequiredService<ApplicationDbContext>();

    var passwordHasher = scope.ServiceProvider
        .GetRequiredService<IPasswordHasher>();

    await AdminSeeder.SeedAsync(
        context,
        passwordHasher);
}


// =====================================================
// HTTP Request Pipeline
// =====================================================

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();