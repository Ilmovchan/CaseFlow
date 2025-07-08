using CaseFlow.DAL.Enums;

namespace CaseFlow.BLL.Dto.Report;

public class ReportDto
{
    public int Id { get; init; }
    public int CaseId { get; init; }
    
    public string Summary { get; set; } = null!;
    public string? Comments { get; set; }
    
    public ApprovalStatus? ApprovalStatus { get; set; }
}