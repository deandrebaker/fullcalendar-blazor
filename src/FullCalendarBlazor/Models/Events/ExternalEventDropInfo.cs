using System.Collections.Generic;
using FullCalendarBlazor.Models.Views;

namespace FullCalendarBlazor.Models.Events
{
    // Todo: Add proper types
    public class ExternalEventDropInfo
    {
        public Event Event { get; set; }

        public IEnumerable<Event> RelatedEvents { get; set; }

        // Todo: Figure out how to use 'revert' property.
        public object DraggedEl { get; set; }
        public View View { get; set; }
    }
}