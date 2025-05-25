using System.Reflection;
using System.Text;
using FluentValidation;
using Mapster;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NoteTakingApp.Endpoints;
using NoteTakingApp.Infrastructure;
using NoteTakingApp.Infrastructure.Services;
using Polly;
using Prometheus;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(Program).Assembly));
TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetExecutingAssembly());
services.AddValidatorsFromAssembly(typeof(Program).Assembly);
services.AddInfrastructure(builder.Configuration);

var jwtSecret = builder.Configuration["JwtSettings:Secret"] ?? throw new ArgumentNullException("JwtSecret is abscent");

var frontendPolicy = "frontend";
services.AddCors(options =>
{
    options.AddPolicy(
        name: frontendPolicy,
        policy =>
        {
            policy.WithOrigins(builder.Configuration["FrontendUrl"]!).AllowAnyMethod().AllowAnyHeader();
        }
    );
});

services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSecret)),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});
services.AddAuthorization();
services.AddSingleton<IJwtTokenGenerator>(new JwtTokenGenerator(jwtSecret));

services.AddEndpointsApiExplorer();
services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description =
            "Enter 'Bearer' followed by your JWT token in the text input below. Example: 'Bearer your_token_here'"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

var app = builder.Build();

app.UseCors(frontendPolicy);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.AddProjectEndpoints();
app.AddNotesEndpoints();
app.AddParagraphsEndpoints();
app.AddUserEndpoints();

app.UseHttpMetrics();
app.MapMetrics().RequireAuthorization();
app.MapHealthChecks("/health");

app.Run();
