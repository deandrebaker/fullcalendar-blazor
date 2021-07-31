using System;
using System.Collections.Generic;
using FullCalendarBlazor.Models.Views;

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
        public View View { get; set; }
        public object El { get; set; }
        public object JsEvent { get; set; }
    }
}