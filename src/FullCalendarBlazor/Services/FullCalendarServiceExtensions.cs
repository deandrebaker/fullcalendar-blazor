using FullCalendarBlazor.JsInterops;
using Microsoft.Extensions.DependencyInjection;

namespace FullCalendarBlazor.Services
{
    public static class FullCalendarServiceExtensions
    {
        public static IServiceCollection AddFullCalendar(this IServiceCollection services)
        {
            services.AddSingleton<IFullCalendarJsInterop, FullCalendarJsInterop>();
            return services;
        }
    }
}
