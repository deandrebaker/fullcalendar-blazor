using System.Collections.Generic;
using System.Linq;

namespace FullCalendarBlazor.Models.Display
{
    public static class ToolbarExtensions
    {
        public static IEnumerable<string> AddAttached(this IEnumerable<string> toolbarOptions, string toolbarOption)
        {
            return toolbarOptions.Append(ToolbarOption.Comma).Append(toolbarOption);
        }

        public static IEnumerable<string> AddDetached(this IEnumerable<string> toolbarOptions, string toolbarOption)
        {
            return toolbarOptions.Append(ToolbarOption.Space).Append(toolbarOption);
        }
    }
}