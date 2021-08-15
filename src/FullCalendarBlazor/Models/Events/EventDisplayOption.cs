using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace FullCalendarBlazor.Models.Events
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum EventDisplayOption
    {
        [EnumMember(Value = "auto")] Auto,
        [EnumMember(Value = "block")] Block,
        [EnumMember(Value = "list-item")] ListItem,
        [EnumMember(Value = "background")] Background,
        [EnumMember(Value = "inverse-background")] InverseBackground,
        [EnumMember(Value = "none")] None,
    }
}