using System;
using CaseFlow.DAL.Enums;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CaseFlow.DAL.Converters;

public class CaseStatusConverter : ValueConverter<CaseStatus, string>
{
    public CaseStatusConverter()
        : base(
            v => ConvertToString(v),
            v => ConvertToEnum(v))
    {
    }

    private static string ConvertToString(CaseStatus status) =>
        status switch
        {
            CaseStatus.Opened => "Відкрито",
            CaseStatus.Closed => "Закрито",
            CaseStatus.Stopped => "Призупинено",
            _ => throw new InvalidOperationException("Unknown case status enum value")
        };

    private static CaseStatus ConvertToEnum(string status) =>
        status switch
        {
            "Відкрито" => CaseStatus.Opened,
            "Закрито" => CaseStatus.Closed,
            "Призупинено" => CaseStatus.Stopped,
            _ => throw new InvalidOperationException("Unknown case status database value")
        };
}