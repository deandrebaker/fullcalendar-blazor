using System;

namespace FullCalendarBlazor.Models.DateAndTime
{
    public class CalendarSelectInfo
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public bool AllDay { get; set; }
        public string ResourceId { get; set; }
    }
}