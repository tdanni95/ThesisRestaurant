using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ThesisRestaurant.Application.Common.Interfaces.Authentication;
using ThesisRestaurant.Application.Common.Interfaces.Persistence;
using ThesisRestaurant.Application.Common.Services;
using ThesisRestaurant.Infrastructure.Authentication;
using ThesisRestaurant.Infrastructure.Persistence.Repositories;
using ThesisRestaurant.Infrastructure.Services;

namespace ThesisRestaurant.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, ConfigurationManager configuration)
        {
            services
                .AddAuth(configuration)
                .AddPersistence(configuration);

            services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
            return services;
        }

        public static IServiceCollection AddPersistence(this IServiceCollection services, ConfigurationManager configuration)
        {
            //services.AddDbContext<BuberDinnerDbContext>(options => options.UseSqlServer("Server=localhost;Database=BuberDinner;User Id=sa;Password=Admin123!;TrustServerCertificate=true"));
            services.AddScoped<IUserRepository, UserRepository>();
            //services.AddScoped<IMenuRepository, MenuRepository>();
            return services;
        }

        public static IServiceCollection AddAuth(this IServiceCollection services, ConfigurationManager configuration)
        {
            var jwtSettings = new JwtSettings();
            configuration.Bind(JwtSettings.SectionName, jwtSettings);

            services.AddSingleton(Options.Create(jwtSettings));
            services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();

            services.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(jwtSettings.Secret))
                });

            return services;
        }
    }
}
