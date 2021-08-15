using System;

namespace FullCalendarBlazor.Models.Events
{
    // Todo: Add types to properties with object types.
    public class EventAllowInfo
    {
        public bool AllDay { get; set; }
        public DateTime End { get; set; }
        public string EndStr { get; set; }
        public object Resource { get; set; }
        public DateTime Start { get; set; }
        public string StartStr { get; set; }
    }
}