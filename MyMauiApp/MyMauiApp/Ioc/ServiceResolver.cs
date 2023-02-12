using MyMauiApp.Services;
using MyMauiApp.View;
using MyMauiApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMauiApp.Ioc
{
    /// <summary>
    /// Used for resolving services.
    /// </summary>
    public static class ServiceResolver
    {
        /// <summary>
        /// Registers services to ioc for application.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection RegisterServices(IServiceCollection services)
        {
            // Pages
            services.AddSingleton<MainPage>();
            services.AddSingleton<BoardsPage>();
            services.AddTransient<BoardPage>();
            services.AddTransient<UpsertBoardPage>();

            // ViewModels
            services.AddSingleton<BoardsViewModel>();
            services.AddTransient<BoardViewModel>();
            services.AddTransient<UpsertBoardViewModel>();

            // Services
            services.AddSingleton<IBoardService, BoardService>();

            return services;
        }

    }
}
