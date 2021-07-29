using System;
using System.Collections.Generic;

namespace FullCalendarBlazor.Models.Events
{
    // Todo: Add proper types
    public class EventDropInfo
    {
        public Event Event { get; set; }
        public IEnumerable<Event> RelatedEvents { get; set; }
        public Event OldEvent { get; set; }
        public object OldResource { get; set; }
        public object NewResource { get; set; }
        public TimeSpan Delta { get; set; }
        // Todo: Figure out how to use 'revert' property.
        public object View { get; set; }
        public object El { get; set; }
        public object JsEvent { get; set; }
    }
}