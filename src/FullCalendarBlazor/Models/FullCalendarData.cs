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
        public bool? Editable { get; set; }
        public bool? EventStartEditable { get; set; }
        public bool? EventResizableFromStart { get; set; }
        public bool? EventDurationEditable { get; set; }
        public bool? EventResourceEditable { get; set; }
        public bool? Droppable { get; set; }
        public int? EventDragMinDistance { get; set; }
        public int? DragRevertDuration { get; set; }
        public bool? DragScroll { get; set; }
        public TimeSpan? SnapDuration  { get; set; }
        public bool? AllDayMaintainDuration { get; set; }
        // Todo: Add FixedMirrorParent property
        public object EventConstraint { get; set; }
    }
}
