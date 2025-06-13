using System;
using CaseFlow.DAL.Enums;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CaseFlow.DAL.Converters;

public class DetectiveStatusConverter : ValueConverter<DetectiveStatus, string>
{
    public DetectiveStatusConverter()
        : base(
            v => ConvertToString(v),
            v => ConvertToEnum(v))
    {
    }

    private static string ConvertToString(DetectiveStatus status) =>
        status switch
        {
            DetectiveStatus.Active => "Активний(а)",
            DetectiveStatus.OnVacation => "У відпустці",
            DetectiveStatus.Retired => "У відставці",
            DetectiveStatus.Fired => "Звільнений(а)",
            _ => throw new InvalidOperationException("Unknown status enum value")
        };

    private static DetectiveStatus ConvertToEnum(string status) =>
        status switch
        {
            "Активний(а)" => DetectiveStatus.Active,
            "У відпустці" => DetectiveStatus.OnVacation,
            "У відставці" => DetectiveStatus.Retired,
            "Звільнений(а)" => DetectiveStatus.Fired,
            _ => throw new InvalidOperationException("Unknown status database value")
        };
}