using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace FullCalendarBlazor.Models.DateAndTime
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum DateAlignmentOption
    {
        [EnumMember(Value = "week")] Week,
        [EnumMember(Value = "month")] Month,
    }
}