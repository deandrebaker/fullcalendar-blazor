using FullCalendarBlazor.Models.Views;

namespace FullCalendarBlazor.Models.Events
{
    // Todo: Add types to properties with object types.
    public class EventRenderInfo
    {
        public Event Event { get; set; }
        public string TimeText { get; set; }
        public bool? IsStart { get; set; }
        public bool? IsEnd { get; set; }
        public bool? IsMirror { get; set; }
        public bool? IsPast { get; set; }
        public bool? IsFuture { get; set; }
        public bool? IsToday { get; set; }
        public object El { get; set; }
        public View View { get; set; }
    }
}