using System;
using CaseFlow.DAL.Enums;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CaseFlow.DAL.Converters;

public class EvidenceTypeConverter : ValueConverter<EvidenceType, string>
{
    public EvidenceTypeConverter()
        : base(
            v => ConvertToString(v),
            v => ConvertToEnum(v))
    {
    }

    private static string ConvertToString(EvidenceType type) =>
        type switch
        {
            EvidenceType.Biometric => "Біометричний доказ",
            EvidenceType.Biological => "Біологічний доказ",
            EvidenceType.Video => "Відеодоказ",
            EvidenceType.Photo => "Фотодоказ",
            EvidenceType.Physical => "Матеріальний доказ",
            EvidenceType.Digital => "Цифровий доказ",
            EvidenceType.Document => "Документальний доказ",
            EvidenceType.Audio => "Аудіодоказ",
            EvidenceType.Object => "Фізичний доказ",
            EvidenceType.Electronic => "Електронний доказ",
            EvidenceType.Other => "Інше",
            _ => throw new InvalidOperationException("Unknown evidence type enum value")
        };

    private static EvidenceType ConvertToEnum(string type) =>
        type switch
        {
            "Біометричний доказ" => EvidenceType.Biometric,
            "Біологічний доказ" => EvidenceType.Biological,
            "Відеодоказ" => EvidenceType.Video,
            "Фотодоказ" => EvidenceType.Photo,
            "Матеріальний доказ" => EvidenceType.Physical,
            "Цифровий доказ" => EvidenceType.Digital,
            "Документальний доказ" => EvidenceType.Document,
            "Аудіодоказ" => EvidenceType.Audio,
            "Фізичний доказ" => EvidenceType.Object,
            "Електронний доказ" => EvidenceType.Electronic,
            "Інше" => EvidenceType.Other,
            _ => throw new InvalidOperationException("Unknown evidence type database value")
        };
}