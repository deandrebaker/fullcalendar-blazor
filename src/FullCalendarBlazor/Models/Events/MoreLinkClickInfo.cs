using System;
using System.Collections.Generic;

namespace FullCalendarBlazor.Models.Events
{
    // Todo: Add types to properties with object types.
    public class MoreLinkClickInfo
    {
        public DateTime Date { get; set; }
        public IEnumerable<EventSegment> AllSegs { get; set; }
        public IEnumerable<EventSegment> HiddenSegs { get; set; }
        public object JsEvent { get; set; }
    }
}