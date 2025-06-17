using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using NpgsqlTypes;

namespace CaseFlow.DAL.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ApprovalStatus
{
    [EnumMember(Value = "Чернетка")]
    [PgName("Чернетка")]
    Чернетка,
    
    [EnumMember(Value = "Надіслано")]
    [PgName("Надіслано")]
    Надіслано,
    
    [EnumMember(Value = "Схвалено")]
    [PgName("Схвалено")]
    Схвалено,
    
    [EnumMember(Value = "Відхилено")]
    [PgName("Відхилено")]
    Відхилено,
}
