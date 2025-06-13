using System;
using CaseFlow.DAL.Enums;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CaseFlow.DAL.Converters;

public class ApprovalStatusConverter : ValueConverter<ApprovalStatus, string>
{
    public ApprovalStatusConverter()
        : base(
            v => ConvertToString(v),
            v => ConvertToEnum(v))
    {
    }

    private static string ConvertToString(ApprovalStatus status) =>
        status switch
        {
            ApprovalStatus.Draft => "Чернетка",
            ApprovalStatus.Submitted => "Надіслано",
            ApprovalStatus.Approved => "Схвалено",
            ApprovalStatus.Rejected => "Відхилено",
            _ => throw new InvalidOperationException("Unknown approval status enum value")
        };

    private static ApprovalStatus ConvertToEnum(string status) =>
        status switch
        {
            "Чернетка" => ApprovalStatus.Draft,
            "Надіслано" => ApprovalStatus.Submitted,
            "Схвалено" => ApprovalStatus.Approved,
            "Відхилено" => ApprovalStatus.Rejected,
            _ => throw new InvalidOperationException("Unknown approval status database value")
        };
}