namespace FullCalendarBlazor.Models.Events
{
    // Todo: Add types to properties with object types.
    public class EventDragInfo
    {
        public Event Event { get; set; }
        public object JsEvent { get; set; }
        public object View { get; set; }
    }
}