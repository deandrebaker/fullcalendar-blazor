using System;
using System.Collections.Generic;

namespace FullCalendarBlazor.Models.Events
{
    public class MoreLinkClickInfo
    {
        public DateTime Date { get; set; }
        public IEnumerable<EventSegment> AllSegs { get; set; }
        public IEnumerable<EventSegment> HiddenSegs { get; set; }
        public object JsEvent { get; set; } // Todo
    }
}