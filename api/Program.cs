using System.Text;
using dotenv.net;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RiskExposureTracker.Models;
using RiskExposureTracker.Repositories;
using RiskExposureTracker.Services;

namespace RiskExposureTracker
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder
                .Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(
                        new System.Text.Json.Serialization.JsonStringEnumConverter()
                    );
                });

            // Load variables from .env (probe parent directories for repo-root .env)
            DotEnv.Fluent().WithProbeForEnv().Load();

            var dbHost = Environment.GetEnvironmentVariable("DB_HOST");
            var dbName = Environment.GetEnvironmentVariable("DB_NAME");
            var dbUser = Environment.GetEnvironmentVariable("DB_USER");
            var dbPassword = Environment.GetEnvironmentVariable("DB_PASSWORD");

            var connStr =
                $"Server={dbHost};Database={dbName};User ID={dbUser};Password={dbPassword};Encrypt=True;TrustServerCertificate=True;Connection Timeout=60";
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connStr, sqlOptions => sqlOptions.EnableRetryOnFailure())
            );

            builder
                .Services.AddIdentity<OrgModel, IdentityRole>(options =>
                {
                    options.Password.RequiredLength = 8;
                    options.User.RequireUniqueEmail = true;
                    options.SignIn.RequireConfirmedAccount = false;
                })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // Dependency injection
            builder.Services.AddScoped<IOrganizationRepository, OrganizationRepository>();
            builder.Services.AddScoped<IOrganizationService, OrganizationService>();
            builder.Services.AddScoped<IRiskRepository, RiskRepository>();
            builder.Services.AddScoped<IRiskService, RiskService>();

            // Add CORS policy here
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(
                    "AllowReactApp",
                    policy =>
                    {
                        policy
                            .WithOrigins("http://localhost:5173") // React app URL
                            .AllowAnyHeader()
                            .AllowAnyMethod()
                            .DisallowCredentials();
                    }
                );
            });

            // Bind JWT options from environment
            var jwtIssuer = Environment.GetEnvironmentVariable("JWT_ISSUER") ?? "ret-api";
            var jwtAudience = Environment.GetEnvironmentVariable("JWT_AUDIENCE") ?? "ret-client";
            var jwtSigningKey =
                Environment.GetEnvironmentVariable("JWT_SIGNING_KEY")
                ?? throw new Exception("JWT_SIGNING_KEY is not set");

            builder.Services.Configure<JwtOptions>(opts =>
            {
                opts.Issuer = jwtIssuer;
                opts.Audience = jwtAudience;
                opts.SigningKey = jwtSigningKey;
                opts.AccessTokenMinutes = 60;
            });

            builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();

            // Configure JWT Bearer authentication
            var key = Encoding.UTF8.GetBytes(jwtSigningKey);
            builder
                .Services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = true;
                    options.SaveToken = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = jwtIssuer,
                        ValidateAudience = true,
                        ValidAudience = jwtAudience,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.FromMinutes(1),
                    };
                });

            var app = builder.Build();

            app.UseCors("AllowReactApp");

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            using (var scope = app.Services.CreateScope())
            {
                await Data.SeedData.InitializeAsync(scope.ServiceProvider);
            }
            app.Run();
        }
    }
}
