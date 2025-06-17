using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using NpgsqlTypes;

namespace CaseFlow.DAL.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum DetectiveStatus
{
    [EnumMember(Value = "Активний(а)")]
    [PgName("Активний(а)")]
    Active,

    [EnumMember(Value = "У відпустці")]
    [PgName("У відпустці")]
    OnVacation,

    [EnumMember(Value = "У відставці")]
    [PgName("У відставці")]
    Retired,

    [EnumMember(Value = "Звільнений(а)")]
    [PgName("Звільнений(а)")]
    Fired,
}