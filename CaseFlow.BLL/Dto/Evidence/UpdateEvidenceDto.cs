using CaseFlow.DAL.Enums;

namespace CaseFlow.BLL.Dto.Evidence;

public class UpdateEvidenceDto
{
    public EvidenceType? Type { get; set; }
    public string? Description { get; set; }
    public DateTime? CollectionDate { get; set; }
    public string? Region { get; set; }
    public string? Annotation { get; set; }
    public string? Purpose { get; set; } 
}
