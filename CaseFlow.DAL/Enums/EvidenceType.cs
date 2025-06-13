using System.Runtime.Serialization;
using NpgsqlTypes;

namespace CaseFlow.DAL.Enums;

public enum EvidenceType
{
    [EnumMember(Value = "Біометричний доказ")]
    [PgName("Біометричний доказ")]
    Biometric,

    [EnumMember(Value = "Біологічний доказ")]
    [PgName("Біологічний доказ")]
    Biological,

    [EnumMember(Value = "Відеодоказ")]
    [PgName("Відеодоказ")]
    Video,

    [EnumMember(Value = "Фотодоказ")]
    [PgName("Фотодоказ")]
    Photo,

    [EnumMember(Value = "Матеріальний доказ")]
    [PgName("Матеріальний доказ")]
    Physical,

    [EnumMember(Value = "Цифровий доказ")]
    [PgName("Цифровий доказ")]
    Digital,

    [EnumMember(Value = "Документальний доказ")]
    [PgName("Документальний доказ")]
    Document,

    [EnumMember(Value = "Аудіодоказ")]
    [PgName("Аудіодоказ")]
    Audio,

    [EnumMember(Value = "Фізичний доказ")]
    [PgName("Фізичний доказ")]
    Object,

    [EnumMember(Value = "Електронний доказ")]
    [PgName("Електронний доказ")]
    Electronic,
    
    [EnumMember(Value = "Інше")]
    [PgName("Інше")]
    Other
}
