using Microsoft.Extensions.DependencyInjection;
using CarService.BL.Interfaces;
using CarService.BL.Services;
using CarService.DL;

namespace CarService.BL
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddBusinessLogic(this IServiceCollection services)
        {
            services.AddDataLayer();
            services.AddSingleton<ICustomerService, CustomerService>();
            return services;
        }
    }
}