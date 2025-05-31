using System.Runtime.Serialization;
using NpgsqlTypes;

namespace CaseFlow.DAL.Enums;

public enum ApprovalStatus
{
    [PgName("Чернетка")]
    [EnumMember(Value = "Чернетка")]
    Draft,
    
    [PgName("Надіслано")]
    [EnumMember(Value = "Надіслано")]
    Submitted,
    
    [PgName("Схвалено")]
    [EnumMember(Value = "Схвалено")]
    Approved,
    
    [PgName("Відхилено")]
    [EnumMember(Value = "Відхилено")]
    Rejected,
}
