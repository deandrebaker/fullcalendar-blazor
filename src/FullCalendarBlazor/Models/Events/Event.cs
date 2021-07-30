using System;
using System.Collections.Generic;

namespace FullCalendarBlazor.Models.Events
{
    /// <summary>
    /// See https://fullcalendar.io/docs/event-parsing for more information.
    /// </summary>
    public class Event
    {
        public string Id { get; set; }
        public string GroupId { get; set; }
        public bool? AllDay { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }
        public IEnumerable<DayOfWeek> DaysOfWeek { get; set; }
        public TimeSpan? StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }
        public DateTime? StartRecur { get; set; }
        public DateTime? EndRecur { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string ClassName { get; set; }
        public IEnumerable<string> ClassNames { get; set; }
        public bool? Editable { get; set; }
        public bool? StartEditable { get; set; }
        public bool? DurationEditable { get; set; }
        public bool? ResourceEditable { get; set; } // Requires one of the resource plugins
        public string ResourceId { get; set; } // Requires one of the resource plugins
        public List<string> ResourceIds { get; set; } // Requires one of the resource plugins
        public string Display { get; set; }
        public bool? Overlap { get; set; }
        public string Constraint { get; set; }
        public string Color { get; set; }
        public string BackgroundColor { get; set; }
        public string BorderColor { get; set; }
        public string TextColor { get; set; }
        public string Rrule { get; set; }
        public object ExtendedProps { get; set; }
    }
}