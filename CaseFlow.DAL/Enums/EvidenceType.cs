using System.Runtime.Serialization;

namespace CaseFlow.DAL.Enums;

public enum EvidenceType
{
    [EnumMember(Value = "Біометричний доказ")]
    Biometric,

    [EnumMember(Value = "Біологічний доказ")]
    Biological,

    [EnumMember(Value = "Відеодоказ")]
    Video,

    [EnumMember(Value = "Фотодоказ")]
    Photo,

    [EnumMember(Value = "Матеріальний доказ")]
    Physical,

    [EnumMember(Value = "Цифровий доказ")]
    Digital,

    [EnumMember(Value = "Документальний доказ")]
    Document,

    [EnumMember(Value = "Аудіодоказ")]
    Audio,

    [EnumMember(Value = "Фізичний доказ")]
    Object,

    [EnumMember(Value = "Електронний доказ")]
    Electronic,
    
    [EnumMember(Value = "Інше")]
    Other
}
