using BackInfinity.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using BackInfinity.Services.Contract;
using BackInfinity.Services.Implementation;
using BackInfinity.Utilitles;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



builder.Services.AddDbContext<DbinfinityContext>(op =>
op.UseSqlServer(builder.Configuration.GetConnectionString("Connection")));
builder.Services.AddControllers();

builder.Services.AddScoped<IAuthorizationService, AuthorizationService>();
builder.Services.AddScoped<IAccessService, AccessService>();
builder.Services.AddScoped<IAppisService, AppisService>();  
builder.Services.AddScoped<IServicesService, ServicesService>();
builder.Services.AddScoped<IAppointmentService, AppointmentService>();

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

//jwt

var key = builder.Configuration.GetValue<string>("JwtSettings:key");

    var keyBites = Encoding.ASCII.GetBytes(key);
    builder.Services.AddAuthentication(confg =>
    {
        confg.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        confg.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

    }).AddJwtBearer(config =>
    {
        config.RequireHttpsMetadata = false;
        config.SaveToken = true;
        config.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(keyBites),
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };
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
