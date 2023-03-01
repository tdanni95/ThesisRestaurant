using Microsoft.AspNetCore.Mvc.Infrastructure;
using ThesisRestaurant.Api.Common.Errors;
using ThesisRestaurant.Api.Common.Mapping;

namespace ThesisRestaurant.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services)
        {
            services.AddControllers();
            services.AddSingleton<ProblemDetailsFactory, ThesisRestaurantProblemDetailsFactory>();
            services.AddMappings();
            return services;
        }
    }
}
