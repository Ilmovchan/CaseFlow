using System.Runtime.Serialization;
using NpgsqlTypes;

namespace CaseFlow.DAL.Enums;

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
    Stopped,
}