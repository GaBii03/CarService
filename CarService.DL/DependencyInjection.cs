using Microsoft.Extensions.DependencyInjection;
using CarService.DL.Interfaces;
using CarService.DL.Repositorities;

namespace CarService.DL
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDataLayer(this IServiceCollection services)
        {
            services.AddSingleton<ICustomerRepository, CustomerStaticRepository>();
            return services;
        }
    }
}

