namespace FullCalendarBlazor.Models.Events
{
    // Todo: Add types to properties with object types.
    public class EventClickInfo
    {
        public Event Event { get; set; }
        public object El { get; set; }
        public object JsEvent { get; set; }
        public object View { get; set; }
    }
}