using System;
using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace FullCalendarBlazor.JsInterops
{
    public class FullCalendarJsInterop : IFullCalendarJsInterop, IAsyncDisposable
    {
        private readonly Lazy<Task<IJSObjectReference>> _moduleTask;
        
        public FullCalendarJsInterop(IJSRuntime jsRuntime)
        {
            _moduleTask = new (() => jsRuntime.InvokeAsync<IJSObjectReference>(
                "import", "./_content/FullCalendarBlazor/fullCalendarJsInterop.js").AsTask());
        }

        public async ValueTask Render(string elementId)
        {
            var module = await _moduleTask.Value;
            await module.InvokeVoidAsync("render", elementId);
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
