using CaseFlow.DAL.Enums;

namespace CaseFlow.BLL.Dto.Evidence;

public class GetEvidenceDto
{
    public EvidenceType Type { get; set; }
    public string Description { get; set; } = null!;
    public DateTime CollectionDate { get; set; }
    public string Region { get; set; } = null!;
    public string? Annotation { get; set; }
    public string? Purpose { get; set; } 
    public int CaseId { get; set; }
    public int EvidenceId { get; set; }
}