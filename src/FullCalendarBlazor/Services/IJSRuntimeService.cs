using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace FullCalendarBlazor.Services
{
    public interface IJSRuntimeService
    {
        ValueTask RenderAsync(string elementId, object calendarData, DotNetObjectReference<FullCalendar> objRef);
        ValueTask ExecuteVoidMethodAsync(string elementId, string methodName, params object[] args);
        ValueTask<TValue> ExecuteMethodAsync<TValue>(string elementId, string methodName, params object[] args);
        ValueTask<TValue> GetPropertyAsync<TValue>(string elementId, string propName);
        ValueTask PrintAsync(object obj);
    }
}