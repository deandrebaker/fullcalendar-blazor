using System.Threading.Tasks;

namespace FullCalendarBlazor
{
    public interface IFullCalendarJsInterop
    {
        ValueTask Render(string elementId);
    }
}
