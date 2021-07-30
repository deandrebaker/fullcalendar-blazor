using System;

namespace FullCalendarBlazor.Models.Events
{
    // Todo: Add types to properties with object types.
    public class DropInfo
    {
        public bool AllDay { get; set; }
        public DateTime Date { get; set; }
        public string DateStr { get; set; }
        public object DraggedEl { get; set; }
        public object JsEvent { get; set; }
        public object Resource { get; set; }
        public object View { get; set; }
    }
}