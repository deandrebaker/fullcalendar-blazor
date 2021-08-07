using System.Collections.Generic;

namespace FullCalendarBlazor.Models.Display
{
    public class Toolbar
    {
        public IEnumerable<ToolbarOption> Start { get; set; }
        public IEnumerable<ToolbarOption> End { get; set; }
        public IEnumerable<ToolbarOption> Left { get; set; }
        public IEnumerable<ToolbarOption> Right { get; set; }
        public IEnumerable<ToolbarOption> Center { get; set; }
    }
}