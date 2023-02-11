using MyMauiApp.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMauiApp.Ioc
{
    public static class ServiceResolver
    {
        /// <summary>
        /// Registers services to ioc for application.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection RegisterServices(IServiceCollection services)
        {
            // Register your services here
            services.AddSingleton<MainPage>();
            services.AddSingleton<BoardsPage>();

            services.AddTransient<BoardPage>();

            return services;
        }

    }
}
