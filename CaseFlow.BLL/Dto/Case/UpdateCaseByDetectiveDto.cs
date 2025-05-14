using CaseFlow.DAL.Enums;

namespace CaseFlow.BLL.Dto.Case;

public class UpdateCaseByDetectiveDto
{
    public int Id { get; init; }

    public string? Description { get; set; }

    public CaseStatus? Status { get; set; }
}