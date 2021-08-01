using System;
using FullCalendarBlazor.Models.Views;

namespace FullCalendarBlazor.Models.DateAndTime
{
    public class DateInfo
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string StartStr { get; set; }
        public string EndStr { get; set; }
        public string TimeZone { get; set; }
        public View View { get; set; }
    }
}