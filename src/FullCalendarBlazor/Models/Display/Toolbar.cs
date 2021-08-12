using System.Collections.Generic;

namespace FullCalendarBlazor.Models.Display
{
    public class Toolbar
    {
        public IEnumerable<string> Start { get; set; }
        public IEnumerable<string> End { get; set; }
        public IEnumerable<string> Left { get; set; }
        public IEnumerable<string> Right { get; set; }
        public IEnumerable<string> Center { get; set; }
    }
}