using System.Collections.Generic;
using System.Linq;

namespace FullCalendarBlazor.Models.Display
{
    public static class ToolbarExtensions
    {
        public static IEnumerable<ToolbarOption> AddAttached(this IEnumerable<ToolbarOption> toolbarOptions, ToolbarOption toolbarOption)
        {
            return toolbarOptions.Append(ToolbarOption.Comma).Append(toolbarOption);
        }

        public static IEnumerable<ToolbarOption> AddDetached(this IEnumerable<ToolbarOption> toolbarOptions, ToolbarOption toolbarOption)
        {
            return toolbarOptions.Append(ToolbarOption.Space).Append(toolbarOption);
        }
    }
}