using System.Collections.Generic;

namespace FullCalendarBlazor.Models.Events
{
    public class EventAddInfo
    {
        public Event Event { get; set; }
        public IEnumerable<Event> RelatedEvents { get; set; }
        // Todo: Figure out how to use 'revert' property.
    }
}