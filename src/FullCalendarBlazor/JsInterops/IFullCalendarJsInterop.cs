using System.Threading.Tasks;

namespace FullCalendarBlazor.JsInterops
{
    public interface IFullCalendarJsInterop
    {
        ValueTask Render(string elementId);
    }
}
