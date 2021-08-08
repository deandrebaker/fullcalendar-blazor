using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace FullCalendarBlazor.Models.Views
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ViewOption
    {
        [EnumMember(Value = "dayGridMonth")] DayGridMonth,
        [EnumMember(Value = "dayGridWeek")] DayGridWeek,
        [EnumMember(Value = "dayGridDay")] DayGridDay,
        [EnumMember(Value = "timeGridWeek")] TimeGridWeek,
        [EnumMember(Value = "timeGridDay")] TimeGridDay,
        [EnumMember(Value = "listYear")] ListYear,
        [EnumMember(Value = "listMonth")] ListMonth,
        [EnumMember(Value = "listWeek")] ListWeek,
        [EnumMember(Value = "listDay")] ListDay,
    }
}