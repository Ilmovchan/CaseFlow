namespace CaseFlow.BLL.Dto.Case;

public class CreateCaseDto
{
    public int ClientId { get; set; }
    
    public int? DetectiveId { get; set; }

    public int CaseTypeId { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public DateOnly DeadlineDate { get; set; }
}