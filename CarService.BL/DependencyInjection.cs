using CarService.BL.Interfaces;
using CarService.BL.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CarService.BL
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddBusinessLogicLayer(this IServiceCollection services)
        {
            // Register BL services here
            services.AddSingleton<ICustomerService, CustomerService>();
            services.AddSingleton<ICarService, Services.CarService>();
            return services;
        }
    }
}