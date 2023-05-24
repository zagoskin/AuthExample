using AuthExample.API.Configuration;
using AuthExample.API.Constants;
using AuthExample.API.Extensions;
using AuthExample.Domain;
using AuthExample.Infrastructure.Contexts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = false,
        ValidateLifetime = false,
        RequireExpirationTime = false,        
        ValidIssuer = builder.Configuration["Authentication:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Authentication:Key"]!))
    };
}).AddApiKeySupport(); 




builder.Services.AddAuthorization(options =>
{
    options.AddPolicy(Identity.RequireAdminRoleName, 
        policy => policy.RequireClaim(ClaimTypes.Role, Identity.AdminRoleName));
});





builder.Services.ConfigureDomain();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.ConfigureExceptionHandler();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
