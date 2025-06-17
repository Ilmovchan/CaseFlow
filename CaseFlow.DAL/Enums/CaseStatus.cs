using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using NpgsqlTypes;

namespace CaseFlow.DAL.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum CaseStatus
{
    [EnumMember(Value = "Відкрито")]
    [PgName("Відкрито")]
    Opened,
    
    [EnumMember(Value = "Закрито")]
    [PgName("Закрито")]
    Closed,
    
    [EnumMember(Value = "Призупинено")]
    [PgName("Призупинено")]
    Paused,
}