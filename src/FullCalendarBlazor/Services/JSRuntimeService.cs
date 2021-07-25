using System;
using System.Threading.Tasks;
using FullCalendarBlazor.Models;
using Microsoft.JSInterop;

namespace FullCalendarBlazor.Services
{
    public class JSRuntimeService : IJSRuntimeService, IAsyncDisposable
    {
        private readonly Lazy<Task<IJSObjectReference>> _moduleTask;
        
        public JSRuntimeService(IJSRuntime jsRuntime)
        {
            _moduleTask = new (() => jsRuntime.InvokeAsync<IJSObjectReference>(
                "import", "./_content/FullCalendarBlazor/fullCalendarJsInterop.js").AsTask());
        }

        public async ValueTask Render(string elementId, FullCalendarData data)
        {
            var module = await _moduleTask.Value;
            await module.InvokeVoidAsync("render", elementId, data);
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