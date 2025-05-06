using System.Runtime.Serialization;

namespace CaseFlow.DAL.Enums;

public enum CaseStatus
{
    [EnumMember(Value = "Відкрито")]
    Opened,
    
    [EnumMember(Value = "Закрито")]
    Closed,
    
    [EnumMember(Value = "Призупинено")]
    Stopped,
}