using CaseFlow.DAL.Enums;

namespace CaseFlow.BLL.Dto.Case;

public class UpdateCaseByAdminDto
{
    public int? CaseTypeId { get; set; }

    public int? ClientId { get; set; }

    public int? DetectiveId { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public DateOnly? StartDate { get; set; }

    public DateOnly? DeadlineDate { get; set; }

    public DateOnly? CloseDate { get; set; }

    public CaseStatus? Status { get; set; }
}