using Microsoft.EntityFrameworkCore;
using inventory_cloud_api.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Identity.Web;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// ======================
// Services
// ======================

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// ======================
// Swagger (NO OAuth for now)
// ======================

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "inventory-cloud-api",
        Version = "v1"
    });
});

// ======================
// Database
// ======================

// builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        sqlOptions =>
        {
            sqlOptions.EnableRetryOnFailure(
                maxRetryCount: 5,
                maxRetryDelay: TimeSpan.FromSeconds(10),
                errorNumbersToAdd: null);
        });

// ======================
// Authentication
// ======================

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));

builder.Services.AddAuthorization();

// ======================
// Build
// ======================

var app = builder.Build();

// ======================
// Middleware
// ======================

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthentication();
app.UseAuthorization();

// ======================
// Endpoints
// ======================

app.MapControllers();

app.MapGet("/", () => "StockFlow API is running");
app.MapGet("/health", () => Results.Ok("Healthy"));

// ======================
// Run
// ======================

app.Run();