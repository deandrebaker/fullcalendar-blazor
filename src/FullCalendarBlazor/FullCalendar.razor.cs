using System;
using System.Collections;
using System.Threading.Tasks;
using FullCalendarBlazor.Models;
using FullCalendarBlazor.Models.Events;
using FullCalendarBlazor.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace FullCalendarBlazor
{
    public partial class FullCalendar
    {
        [Inject] private IJSRuntimeService JsInterop { get; set; }
        [Parameter] public string Id { get; set; }
        [Parameter] public IEnumerable Events { get; set; }
        [Parameter] public Action<EventActionArgs> OnEventClick { get; set; }

        [JSInvokable]
        public void EventClick(EventActionArgs e)
        {
            OnEventClick?.Invoke(e);
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            var data = new FullCalendarData
            {
                Events = Events
            };
            await JsInterop.Render(Id, data, DotNetObjectReference.Create(this));
        }
    }
}