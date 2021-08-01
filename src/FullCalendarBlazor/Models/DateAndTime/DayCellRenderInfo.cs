using System;

namespace FullCalendarBlazor.Models.DateAndTime
{
    public class DayCellRenderInfo
    {
        public DateTime Date { get; set; }
        public string DayNumberText { get; set; }
        public bool IsPast { get; set; }
        public bool IsFuture { get; set; }
        public bool IsToday { get; set; }
        public bool IsOther { get; set; }
        public object Resource { get; set; } // Todo
        public object El { get; set; } // Todo
    }
}