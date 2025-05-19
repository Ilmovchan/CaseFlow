using CaseFlow.DAL.Enums;

namespace CaseFlow.BLL.Dto.Expense;

public class UpdateExpenseDto
{
    public int Id { get; init; }
    
    public DateTime DateTime { get; set; }
    public string Purpose { get; set; } = null!;
    public decimal Amount { get; set; }
    public string? Annotation { get; set; }
    public ApprovalStatus Status { get; set; } = ApprovalStatus.Draft;
}