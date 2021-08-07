namespace FullCalendarBlazor.Models.DateAndTime
{
    public class DateTimeFormatter
    {
        public DateTimeFormatterOption? Year { get; set; }
        public DateTimeFormatterOption? Month { get; set; }
        public DateTimeFormatterOption? Day { get; set; }
        public DateTimeFormatterOption? Weekday { get; set; }
        public DateTimeFormatterOption? Hour { get; set; }
        public DateTimeFormatterOption? Minute { get; set; }
        public DateTimeFormatterOption? Second { get; set; }
        public bool? Hour12 { get; set; }
        public DateTimeFormatterOption? TimeZoneName { get; set; }
        public DateTimeFormatterOption? Week { get; set; }
        public DateTimeFormatterOption? Meridiem { get; set; }
        public bool? OmitZeroMinute { get; set; }
        public bool? OmitCommas { get; set; }
        public bool? OmitMeridiem { get; set; }
    }
}