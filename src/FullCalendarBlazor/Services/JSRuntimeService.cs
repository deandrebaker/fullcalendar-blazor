using System;
using System.Threading.Tasks;
using FullCalendarBlazor.Models;
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

        public async ValueTask Render(string elementId, FullCalendarData data,
            DotNetObjectReference<FullCalendar> objRef)
        {
            var serializedData = JsonConvert.SerializeObject(data, Formatting.Indented, new JsonSerializerSettings
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