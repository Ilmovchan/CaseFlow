using System.Runtime.Serialization;

namespace CaseFlow.DAL.Enums;

public enum DetectiveStatus
{
    [EnumMember(Value = "Активний(а)")]
    Active,

    [EnumMember(Value = "У відпустці")]
    OnVacation,

    [EnumMember(Value = "У відставці")]
    Retired,

    [EnumMember(Value = "Звільнений(а)")]
    Fired,
}