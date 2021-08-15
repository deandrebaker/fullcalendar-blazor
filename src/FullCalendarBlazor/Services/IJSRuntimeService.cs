using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace FullCalendarBlazor.Services
{
    public interface IJSRuntimeService
    {
        ValueTask RenderAsync(object calendarData, IEnumerable<(string, string)> calendarMethods, DotNetObjectReference<IJSInvokableService> objRef);
        ValueTask ExecuteVoidMethodAsync(string elementId, string methodName, params object[] args);
        ValueTask<TValue> ExecuteMethodAsync<TValue>(string elementId, string methodName, params object[] args);
        ValueTask ExecuteVoidEventMethodAsync(string elementId, string eventId, string methodName, params object[] args);
        ValueTask<TValue> ExecuteEventMethodAsync<TValue>(string elementId, string eventId, string methodName, params object[] args);
        ValueTask<TValue> GetPropertyAsync<TValue>(string elementId, string propName);
        ValueTask PrintAsync(object obj);
    }
}