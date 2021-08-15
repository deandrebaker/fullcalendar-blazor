using System;
using FullCalendarBlazor.Models.Views;

namespace FullCalendarBlazor.Models.DateAndTime
{
    public class SelectionInfo
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string StartStr { get; set; }
        public string EndStr { get; set; }
        public bool AllDay { get; set; }
        public object JsEvent { get; set; }
        public View View { get; set; }
        public object Resource { get; set; }
    }
}