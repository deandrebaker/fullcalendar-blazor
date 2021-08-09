using System;

namespace FullCalendarBlazor.Models.Events
{
    public class EventSegment
    {
        public Event Event { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public bool IsStart { get; set; }
        public bool IsEnd { get; set; }
    }
}