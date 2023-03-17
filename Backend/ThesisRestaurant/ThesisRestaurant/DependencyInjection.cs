using Microsoft.AspNetCore.Mvc.Infrastructure;
using ThesisRestaurant.Api.Common.Errors;
using ThesisRestaurant.Api.Common.Mapping;

namespace ThesisRestaurant.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services, ConfigurationManager configuration)
        {
            services.AddControllers();
            services.AddSingleton<ProblemDetailsFactory, ThesisRestaurantProblemDetailsFactory>();
            services.AddMappings();
            services.AddHttpContextAccessor();
            services.AddCors(configuration);

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => false;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            return services;
        }

        public static IServiceCollection AddCors(this IServiceCollection services, ConfigurationManager configuration)
        {
            var policyName = configuration.GetValue<string>("corsname");

            services.AddCors(options =>
            {
                options.AddPolicy(name: policyName!,
                    builder =>
                    {
                        builder
                        .WithOrigins("http://localhost:5173/")
                        //.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowCredentials()
                        .AllowAnyHeader()
                        .SetIsOriginAllowed(origin => true);
                    });
            });
            return services;
        }
    }
}
