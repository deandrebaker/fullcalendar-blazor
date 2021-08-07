using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace FullCalendarBlazor.Models.Display
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ToolbarOption
    {
        [EnumMember(Value = " ")] Space,
        [EnumMember(Value = "")] Empty,
        [EnumMember(Value = ",")] Comma,
        [EnumMember(Value = "title")] Title,
        [EnumMember(Value = "prev")] Prev,
        [EnumMember(Value = "next")] Next,
        [EnumMember(Value = "prevYear")] PrevYear,
        [EnumMember(Value = "nextYear")] NextYear,
        [EnumMember(Value = "today")] Today,
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