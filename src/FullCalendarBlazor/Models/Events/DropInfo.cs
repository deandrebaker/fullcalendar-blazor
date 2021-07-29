using System;

namespace FullCalendarBlazor.Models.Events
{
    /// <summary>
    /// See https://fullcalendar.io/docs/eventAllow for more information.
    /// </summary>
    public class DropInfo
    {
        public bool AllDay { get; set; }
        public DateTime End { get; set; }
        public string EndStr { get; set; }
        public object Resource { get; set; } // Todo: Switch object to Resource type
        public DateTime Start { get; set; }
        public string StartStr { get; set; }
    }
}