using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SP.WebApi.Services;
using SP.WebApi.Services.Interfaces;

namespace SP.WebApi.Extensions
{
    public static class DependencyInjectionExtension
    {
        
        public static IServiceCollection AddCustomServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddScoped<IEmployeeService,EmployeeService>();

            return services;

        }
    }
}
