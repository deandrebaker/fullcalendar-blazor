using System;
using System.Collections.Generic;

namespace FullCalendarBlazor.Models.Events
{
    public class EventResizeInfo
    {
        public Event Event { get; set; }
        public IEnumerable<Event> RelatedEvents { get; set; }
        public Event OldEvent { get; set; }
        public TimeSpan EndDelta { get; set; }
        public TimeSpan StartDelta { get; set; }
        // Todo: Figure out how to use 'revert' property.
        public object View { get; set; }
        public object El { get; set; }
        public object JsEvent { get; set; }
    }
}