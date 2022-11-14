using System.IO.Compression;
using System.Text.Json.Serialization;
using Aluraflix.Data;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AluraflixContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddResponseCompression(options =>{
        options.Providers.Add<GzipCompressionProvider>();
    });
    builder.Services.Configure<GzipCompressionProviderOptions>(options =>
    {
        options.Level = CompressionLevel.Optimal;
    });

builder.Services.AddControllers()
        .ConfigureApiBehaviorOptions(options =>
        {
            options.SuppressModelStateInvalidFilter = true; //Alterar comportamento da ModelState
        })
        .AddJsonOptions(x =>
        {
            x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            x.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault; //nao renderiza objetos nulos
        });


var app = builder.Build();


app.UseHttpsRedirection();

// app.UseAuthorization();
var port = Environment.GetEnvironmentVariable("PORT") ?? "3000";

app.UseResponseCompression();
app.MapControllers();

app.Run($"http://localhost:{port}");
