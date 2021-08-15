using System;

namespace FullCalendarBlazor.Models.DateAndTime
{
    // Todo: Add types to properties with object types.
    public class DayCellRenderInfo
    {
        public DateTime Date { get; set; }
        public string DayNumberText { get; set; }
        public bool IsPast { get; set; }
        public bool IsFuture { get; set; }
        public bool IsToday { get; set; }
        public bool IsOther { get; set; }
        public object Resource { get; set; }
        public object El { get; set; }
    }
}