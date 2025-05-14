using System.Runtime.Serialization;

namespace CaseFlow.DAL.Enums;

public enum ApprovalStatus
{
    [EnumMember(Value = "Чернетка")]
    Draft,
    
    [EnumMember(Value = "Надіслано")]
    Submitted,
    
    [EnumMember(Value = "Схвалено")]
    Approved,
    
    [EnumMember(Value = "Відхилено")]
    Rejected,
}
