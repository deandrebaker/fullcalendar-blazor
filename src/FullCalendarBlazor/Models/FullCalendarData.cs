using System;
using System.Collections.Generic;
using FullCalendarBlazor.Models.DateAndTime;
using FullCalendarBlazor.Models.Events;
using FullCalendarBlazor.Models.Display;

namespace FullCalendarBlazor.Models
{
    public class FullCalendarData
    {
        // Overall Display
        public Toolbar HeaderToolbar { get; set; }
        public Toolbar FooterToolbar { get; set; }
        public DateTimeFormatter TitleFormat { get; set; }
        public string TitleRangeSeparator { get; set; }
        public Dictionary<string, string> ButtonText { get; set; }
        public Dictionary<string, string> ButtonIcons { get; set; }
        public object CustomButtons { get; set; }
        
        // Events
        public IEnumerable<Event> Events { get; set; }
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
        public TimeSpan? SnapDuration { get; set; }
        public bool? AllDayMaintainDuration { get; set; }
        // Todo: Add FixedMirrorParent property
        public object EventConstraint { get; set; }
    }
}