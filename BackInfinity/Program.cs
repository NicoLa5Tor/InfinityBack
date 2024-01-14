using BackInfinity.Models;
using BackInfinity.Services.Contract;
using BackInfinity.Services.Implementation;
using BackInfinity.Utilitles;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



builder.Services.AddDbContext<DbinfinityContext>(op =>
op.UseSqlServer(builder.Configuration.GetConnectionString("Connection")));
builder.Services.AddControllers();

builder.Services.AddScoped<IServicesService, ServicesService>();

//declare Automapper//
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
//cors//
builder.Services.AddCors(options =>
{
    options.AddPolicy("NewPolitic", add =>
    {
        add.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod();
        add.WithOrigins("http://localhost:4200").AllowCredentials();
    });
});

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("NewPolitic");
app.MapControllers();
app.UseHttpsRedirection();
app.Run();
