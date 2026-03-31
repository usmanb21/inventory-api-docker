var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.MapGet("/", () => "CD is working 🚀");

app.Run();