using System;
using FullCalendarBlazor.Models.Views;

namespace FullCalendarBlazor.Models.DateAndTime
{
    public class DateClickInfo
    {
        public DateTime Date { get; set; }
        public string DateStr { get; set; }
        public bool AllDay { get; set; }
        public object DayEl { get; set; } // Todo
        public object JsEvent { get; set; }
        public View View { get; set; }
        public object Resource { get; set; } // Todo
    }
}