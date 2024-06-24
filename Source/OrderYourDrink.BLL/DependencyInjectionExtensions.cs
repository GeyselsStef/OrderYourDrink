using ChoseYourDrink.BLL;
using Microsoft.Extensions.DependencyInjection;
using OrderYourDrink.BLL.Models;

namespace OrderYourDrink.BLL
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddOrderYourDrinkServices(this IServiceCollection services)
        {
            services.AddSingleton<IQueueService, QueueService>();
            services.AddTransient< ServiceBusSettings>();
            return services;
        }
    }
}
