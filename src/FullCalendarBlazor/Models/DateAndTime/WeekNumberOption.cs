using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace FullCalendarBlazor.Models.DateAndTime
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum WeekNumberOption
    {
        [EnumMember(Value = "local")] Local,
        [EnumMember(Value = "ISO")] Iso,
    }
}