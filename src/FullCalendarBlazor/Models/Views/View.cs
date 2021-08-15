using System;

namespace FullCalendarBlazor.Models.Views
{
    // Todo: Add types to properties with object types.
    public class View
    {
        public string Type { get; set; }
        public string Title { get; set; }
        public DateTime? ActiveStart { get; set; }
        public DateTime? ActiveEnd { get; set; }
        public DateTime? CurrentStart { get; set; }
        public DateTime? CurrentEnd { get; set; }
        public object Calendar { get; set; }
    }
}