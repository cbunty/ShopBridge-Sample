using System.Runtime.Serialization;

namespace ShopBridge.Domain.Enumerations
{
    [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
    public enum StatusEnum
    {
        [EnumMember(Value = "Active")]
        Active = 1,
        [EnumMember(Value = "InActive")]
        InActive
    }
}
