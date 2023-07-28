using JobManagementSystem.API.Middlewares;
using JobManagementSystem.Extentions;
using JobManagementSystem.Repository.RepoModels.Config;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).
    AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
            ValidIssuer = builder.Configuration["Jwt:Issuer"]
        };
    });
builder.Services.Configure<Connection>(builder.Configuration.GetSection("Connection"));
builder.Services.AddCustomServices();
builder.Services.AddSwaggerGen();
var app = builder.Build();
app.UseMiddleware<GlobalErrorHandlerMiddleware>();
// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
