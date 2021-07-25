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
        private FullCalendarData Data { get; set; }

        protected override void OnInitialized()
        {
            Data = new FullCalendarData();
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
