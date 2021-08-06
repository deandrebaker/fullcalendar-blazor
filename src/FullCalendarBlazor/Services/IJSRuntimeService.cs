using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace FullCalendarBlazor.Services
{
    public interface IJSRuntimeService
    {
        ValueTask Render(string elementId, object calendarData, DotNetObjectReference<FullCalendar> objRef);
    }
}