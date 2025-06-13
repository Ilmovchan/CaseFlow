using System.Runtime.Serialization;
using NpgsqlTypes;

namespace CaseFlow.DAL.Enums;

public enum ApprovalStatus
{
    [EnumMember(Value = "Чернетка")]
    [PgName("Чернетка")]
    Draft,
    
    [EnumMember(Value = "Надіслано")]
    [PgName("Надіслано")]
    Submitted,
    
    [EnumMember(Value = "Схвалено")]
    [PgName("Схвалено")]
    Approved,
    
    [EnumMember(Value = "Відхилено")]
    [PgName("Відхилено")]
    Rejected,
}
