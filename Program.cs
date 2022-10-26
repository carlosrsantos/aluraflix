using Aluraflix.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AluraflixContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddControllers();


var app = builder.Build();


app.UseHttpsRedirection();

// app.UseAuthorization();
var port = Environment.GetEnvironmentVariable("PORT") ?? "3000";

app.MapControllers();
app.MapGet("/", () => "Hello World!");

app.Run($"http://localhost:{port}");
