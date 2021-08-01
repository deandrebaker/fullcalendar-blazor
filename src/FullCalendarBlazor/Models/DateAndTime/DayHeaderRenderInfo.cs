using System;

namespace FullCalendarBlazor.Models.DateAndTime
{
    public class DayHeaderRenderInfo
    {
        public DateTime Date { get; set; }
        public string Text { get; set; }
        public bool? IsPast { get; set; }
        public bool? IsFuture { get; set; }
        public bool? IsToday { get; set; }
        public bool? IsOther { get; set; }
        public object Resource { get; set; } // Todo
        public object El { get; set; } // Todo
    }
}