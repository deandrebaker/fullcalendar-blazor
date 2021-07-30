using System.Collections.Generic;

namespace FullCalendarBlazor.Models.Events
{
    // Todo: Add proper types
    public class ExternalEventDropInfo
    {
        public Event Event { get; set; }

        public IEnumerable<Event> RelatedEvents { get; set; }

        // Todo: Figure out how to use 'revert' property.
        public object DraggedEl { get; set; }
        public object View { get; set; }
    }
}