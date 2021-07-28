using Microsoft.AspNetCore.Components;

namespace FullCalendarBlazor.Models.Events
{
    public class EventActionArgs
    {
        public Event Event { get; set; }
        public object El { get; set; }
        public object JsEvent { get; set; }
        public object View { get; set; }
    }
}