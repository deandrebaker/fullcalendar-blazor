using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace FullCalendarBlazor.Models.DateAndTime
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum DateTimeFormatterOption
    {
        [EnumMember(Value = "numeric")] Numeric,
        [EnumMember(Value = "2-digit")] TwoDigit,
        [EnumMember(Value = "long")] Long,
        [EnumMember(Value = "short")] Short,
        [EnumMember(Value = "narrow")] Narrow,
        [EnumMember(Value = "lowercase")] Lowercase,
    }
}