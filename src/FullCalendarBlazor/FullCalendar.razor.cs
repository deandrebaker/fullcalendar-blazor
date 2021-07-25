using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using FullCalendarBlazor.Models;
using FullCalendarBlazor.Services;
using Microsoft.AspNetCore.Components;

namespace FullCalendarBlazor
{
    public partial class FullCalendar
    {
        [Inject] private IJSRuntimeService JsInterop { get; set; }
        [Parameter] public string Id { get; set; }
        [Parameter] public IEnumerable Events { get; set; }
        private FullCalendarData Data { get; set; }

        private void ValidateParameters()
        {
            Events ??= Enumerable.Empty<Event>();
        }

        protected override void OnInitialized()
        {
            ValidateParameters();
            Data = new FullCalendarData {Events = Events};
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await JsInterop.Render(Id, Data);
            }
        }
    }
}
