using System.Threading.Tasks;
using FullCalendarBlazor.Models;
using Microsoft.JSInterop;

namespace FullCalendarBlazor.Services
{
    public interface IJSRuntimeService
    {
        ValueTask Render(string elementId, FullCalendarData data, DotNetObjectReference<FullCalendar> objRef);
    }
}
