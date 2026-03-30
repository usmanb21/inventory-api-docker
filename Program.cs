using Microsoft.AspNetCore.HttpOverrides;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

var app = builder.Build();

// 🔥 IMPORTANT (Azure fix)
app.UseForwardedHeaders();

// Optional but good
// app.UseHttpsRedirection();

app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/", () => "CD is working 🚀");

app.MapControllers();

app.Run();