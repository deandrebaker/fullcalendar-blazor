using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace FullCalendarBlazor
{
    public partial class FullCalendar
    {
        [Inject]
        private IFullCalendarJsInterop JsInterop { get; set; }
        
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await JsInterop.Render();
            }
        }
    }
}