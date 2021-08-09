using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace FullCalendarBlazor.Models.International
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum DirectionOption
    {
        [EnumMember(Value = "ltr")] LeftToRight,
        [EnumMember(Value = "rtl")] RightToLeft,
    }
}