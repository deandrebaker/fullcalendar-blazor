using Microsoft.Extensions.DependencyInjection;

namespace FullCalendarBlazor
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