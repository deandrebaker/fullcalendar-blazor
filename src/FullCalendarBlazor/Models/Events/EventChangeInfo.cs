using System.Collections.Generic;

namespace FullCalendarBlazor.Models.Events
{
    public class EventChangeInfo
    {
        public Event Event { get; set; }
        public IEnumerable<Event> RelatedEvents { get; set; }
        public Event OldEvent { get; set; }
        // Todo: Figure out how to use 'revert' property.
    }
}