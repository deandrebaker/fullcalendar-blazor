using System.Threading.Tasks;
using FullCalendarBlazor.Models;

namespace FullCalendarBlazor.Services
{
    public interface IJSRuntimeService
    {
        ValueTask Render(string elementId, FullCalendarData data);
    }
}
