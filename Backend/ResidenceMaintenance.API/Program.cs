using Microsoft.EntityFrameworkCore;
using ResidenceMaintenance.Data.Context;
using ResidenceMaintenance.Core.Interfaces.Services;
using ResidenceMaintenance.Data.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddDbContext<ResidenceMaintenanceDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IResidenceService, ResidenceService>();
builder.Services.AddScoped<IRoomService, RoomService>();
var app = builder.Build();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();