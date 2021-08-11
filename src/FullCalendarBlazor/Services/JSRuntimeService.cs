using System;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace FullCalendarBlazor.Services
{
    public class JSRuntimeService : IJSRuntimeService, IAsyncDisposable
    {
        private readonly Lazy<Task<IJSObjectReference>> _moduleTask;

        public JSRuntimeService(IJSRuntime jsRuntime)
        {
            _moduleTask = new Lazy<Task<IJSObjectReference>>(() => jsRuntime.InvokeAsync<IJSObjectReference>(
                "import", "./_content/FullCalendarBlazor/fullCalendarJsInterop.js").AsTask());
        }

        public async ValueTask RenderAsync(string elementId, object calendarData, DotNetObjectReference<FullCalendar> objRef)
        {
            var serializedData = JsonConvert.SerializeObject(calendarData, Formatting.Indented, new JsonSerializerSettings
            {
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new CamelCaseNamingStrategy()
                },
                NullValueHandling = NullValueHandling.Ignore
            });
            var module = await _moduleTask.Value;
            await module.InvokeVoidAsync("render", elementId, serializedData, objRef);
        }

        public async ValueTask ExecuteVoidMethodAsync(string elementId, string methodName, params object[] args)
        {
            var module = await _moduleTask.Value;
            await module.InvokeVoidAsync("executeMethod", elementId, methodName, args);
        }

        public async ValueTask<TValue> ExecuteMethodAsync<TValue>(string elementId, string methodName, params object[] args)
        {
            var module = await _moduleTask.Value;
            return await module.InvokeAsync<TValue>("executeMethod", elementId, methodName, args);
        }

        public async ValueTask PrintAsync(object obj)
        {
            var module = await _moduleTask.Value;
            await module.InvokeVoidAsync("print", obj);
        }

        public async ValueTask DisposeAsync()
        {
            if (_moduleTask.IsValueCreated)
            {
                var module = await _moduleTask.Value;
                await module.DisposeAsync();
            }
        }
    }
}