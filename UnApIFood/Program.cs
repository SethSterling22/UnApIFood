using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using UnApIFood.Services;
using UnApIFood.Utils;
using UnApIFood.Repositories;
using Microsoft.OpenApi.Models;
using System.Text;
using Microsoft.AspNetCore.Diagnostics;

string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);

//Configurations  
ConfigUtil.ApiKey = builder.Configuration.GetValue<string>("ApiKey");
ConfigUtil.ConnectionString = builder.Configuration.GetConnectionString("ConnectionString");
ConfigUtil.JWTAudience = builder.Configuration.GetSection("Jwt").GetValue<string>("Audience");
ConfigUtil.JWTKey = builder.Configuration.GetSection("Jwt").GetValue<string>("Key");
ConfigUtil.JWTIssuer = builder.Configuration.GetSection("Jwt").GetValue<string>("Issuer");


// Add services to the container.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
       .AddJwtBearer(options =>
       {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = ConfigUtil.JWTIssuer,
                ValidAudience = ConfigUtil.JWTAudience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ConfigUtil.JWTKey))
            };
       });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
    {
        Description = "API Key is needed",
        Type = SecuritySchemeType.ApiKey,
        Name = "ApiKey",
        In = ParameterLocation.Header
    });
    var scheme = new OpenApiSecurityScheme
    {
        Reference = new OpenApiReference
        {
            Type = ReferenceType.SecurityScheme,
            Id = "ApiKey"
        },
        In = ParameterLocation.Header
    };
    var requirement = new OpenApiSecurityRequirement
    {
        { scheme, new List<string>() }
    };
    c.AddSecurityRequirement(requirement);
});

////////////////////////////////////////////////////////////////////
builder.Services.AddSingleton<UniversitiesService>();
builder.Services.AddSingleton<UsersService>();
builder.Services.AddSingleton<MenusService>();
builder.Services.AddSingleton<PlacesService>();
builder.Services.AddSingleton<LoginService>();
builder.Services.AddSingleton<UsersFavService>();
builder.Services.AddSingleton<UnivDAO>();
builder.Services.AddSingleton<PlaceDAO>();
builder.Services.AddSingleton<MenuDAO>();
builder.Services.AddSingleton<UserDAO>();
builder.Services.AddSingleton<LoginDAO>();
builder.Services.AddSingleton<UserFavDAO>();
////////////////////////////////////////////////////////////////////

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(builder => builder.AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials()
        .SetIsOriginAllowed((host) => true)
        .WithOrigins("http://localhost:4200", "http://localhost:4300"));

// app.UseMiddleware<ExceptionMiddleware>;

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
