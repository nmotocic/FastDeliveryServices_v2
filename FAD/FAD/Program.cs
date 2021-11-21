
using FAD.Domain.Repository;
using FAD.Domain.Services;
using FAD.Repository;
using FAD.Services;
using ConfigurationManager = System.Configuration.ConfigurationManager;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IFlightRepository>(new FlightRepository());
builder.Services.AddSingleton<IDeliveryRepository>(new DeliveryRepository());
builder.Services.AddSingleton<IDeliveryAirportRepository>(new DeliveryAirportRepository());

builder.Services.AddScoped<IFlightService, FlightService>();
builder.Services.AddScoped<IDeliveryService, DeliveryService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
