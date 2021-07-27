using System;
using System.Collections.Generic;
using FullCalendarBlazor.Models;

namespace FullCalendarExamples.Pages
{
    public partial class Index
    {
        private List<Event> Events => new()
        {
            new() {Id = "Birthday", GroupId = "Special Events", Start = DateTime.Now, Title = "Party"},
            new() {Id = "Birthday", GroupId = "Special Events", Start = DateTime.Now + TimeSpan.FromDays(1)}
        };
    }
}
