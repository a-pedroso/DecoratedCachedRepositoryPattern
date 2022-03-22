using Microsoft.Extensions.Caching.Memory;
using WebApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddMemoryCache();

// -- without cached repository
//builder.Services.AddScoped<IWeatherRepository, WeatherRepository>();

// -- with cached repository with .net DI only
//builder.Services.AddScoped<WeatherRepository>();
//builder.Services.AddScoped<IWeatherRepository>(s =>
//{
//    var memoryCache = s.GetRequiredService<IMemoryCache>();
//    var repository = s.GetRequiredService<WeatherRepository>();
//    return new CachedWeatherRepository(memoryCache, repository);
//});

// -- with cached repository with scrutor
builder.Services.AddScoped<IWeatherRepository, WeatherRepository>();
builder.Services.Decorate<IWeatherRepository, CachedWeatherRepository>();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
