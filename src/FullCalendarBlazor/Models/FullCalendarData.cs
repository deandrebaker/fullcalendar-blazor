using System;
using System.Collections;

namespace FullCalendarBlazor.Models
{
    public class FullCalendarData
    {
        public IEnumerable Events { get; set; }
        public bool? DefaultAllDay { get; set; }
        public TimeSpan? DefaultAllDayEventDuration { get; set; }
        public TimeSpan? DefaultTimedEventDuration { get; set; }
        public bool? ForceEventDuration { get; set; }
    }
}
