namespace FullCalendarBlazor.Models.DateAndTime
{
    public class DateTimeFormatter
    {
        public string Year { get; set; }
        public string Month { get; set; }
        public string Day { get; set; }
        public string Weekday { get; set; }
        public string Hour { get; set; }
        public string Minute { get; set; }
        public string Second { get; set; }
        public string Hour12 { get; set; }
        public string TimeZoneName { get; set; }
        public string Week { get; set; }
        public string Meridiem { get; set; }
        public bool OmitZeroMinute { get; set; }
        public bool OmitCommas { get; set; }
    }
}