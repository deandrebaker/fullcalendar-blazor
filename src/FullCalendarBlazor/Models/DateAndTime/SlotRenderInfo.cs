using System;

namespace FullCalendarBlazor.Models.DateAndTime
{
    // Todo: Add types to properties with object types.
    public class SlotRenderInfo
    {
        public DateTime Date { get; set; }
        public string Text { get; set; }
        public bool? IsPast { get; set; }
        public bool? IsFuture { get; set; }
        public bool? IsToday { get; set; }
        public object El { get; set; }
        public object Level { get; set; }
    }
}