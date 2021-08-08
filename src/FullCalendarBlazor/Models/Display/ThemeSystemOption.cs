using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace FullCalendarBlazor.Models.Display
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ThemeSystemOption
    {
        [EnumMember(Value = "standard")] Standard,
        [EnumMember(Value = "bootstrap")] Bootstrap,
    }
}