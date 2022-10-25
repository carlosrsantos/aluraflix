var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
// builder.Services.AddEndpointsApiExplorer();


var app = builder.Build();


// app.UseHttpsRedirection();

// app.UseAuthorization();
var port = Environment.GetEnvironmentVariable("PORT") ?? "3000";

app.MapControllers();
app.MapGet("/", () => "Hello World!");

app.Run($"http://localhost:{port}");
