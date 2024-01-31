using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XafCustomComponents.Services
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBlazorDialog(this IServiceCollection services)
        {
            services.TryAddScoped<IDialogService, DialogService>();
            return services;
        }

        public static IServiceCollection AddNotification(this IServiceCollection services)
        {
            services.TryAddTransient<INotificationService, NotificationService>();
            return services;
        }

        public static IServiceCollection AddXafCustomComponentsServices(this IServiceCollection services)
        {
            return services
                .AddBlazorDialog()
                .AddNotification();
        }
    }
}
