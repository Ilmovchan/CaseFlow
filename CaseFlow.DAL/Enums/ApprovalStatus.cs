using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using NpgsqlTypes;

namespace CaseFlow.DAL.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ApprovalStatus
{
    [EnumMember(Value = "Чернетка")]
    [PgName("Чернетка")]
    Draft,
    
    [EnumMember(Value = "Надіслано")]
    [PgName("Надіслано")]
    Pending,
    
    [EnumMember(Value = "Схвалено")]
    [PgName("Схвалено")]
    Approved,
    
    [EnumMember(Value = "Відхилено")]
    [PgName("Відхилено")]
    Declined,
}