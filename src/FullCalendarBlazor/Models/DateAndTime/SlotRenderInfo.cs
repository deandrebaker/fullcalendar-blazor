using System;

namespace FullCalendarBlazor.Models.DateAndTime
{
    public class SlotRenderInfo
    {
        public DateTime Date { get; set; }
        public string Text { get; set; }
        public bool? IsPast { get; set; }
        public bool? IsFuture { get; set; }
        public bool? IsToday { get; set; }
        public object El { get; set; } // Todo
        public object Level { get; set; } // Todo
    }
}