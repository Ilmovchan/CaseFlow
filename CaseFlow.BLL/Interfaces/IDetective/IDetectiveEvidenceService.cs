using CaseFlow.BLL.Dto.Evidence;
using CaseFlow.DAL.Models;

namespace CaseFlow.BLL.Interfaces.IDetective;

public interface IDetectiveEvidenceService
{
    Task<EvidenceCaseDto> CreateEvidenceAsync(int caseId, CreateEvidenceDto dto, int detectiveId);
    Task<EvidenceCaseDto> UpdateEvidenceAsync
        (int evidenceId, UpdateEvidenceDto dto, int detectiveId, bool forceClone = true);
    Task DeleteEvidenceAsync(int evidenceId, int detectiveId);
    
    Task<List<EvidenceCaseDto>> GetAssignedEvidencesAsync(int detectiveId);
    Task<List<EvidenceCaseDto>> GetUnassignedEvidencesAsync(int detectiveId);
    Task<List<EvidenceCaseDto>> GetEvidencesAsync(int detectiveId);
    Task<List<EvidenceDto>> GetAllEvidencesAsync(int detectiveId);
    Task<List<EvidenceCaseDto>> GetDeclinedEvidencesAsync(int detectiveId);
    Task<List<EvidenceCaseDto>> GetApprovedEvidencesAsync(int detectiveId);
    Task<EvidenceCaseDto?> GetEvidenceAsync(int evidenceId, int detectiveId);
    Task<List<EvidenceCaseDto>> GetEvidencesFromCase(int caseId, int detectiveId);
    
    Task<List<EvidenceCaseDto>> GetPendingEvidencesAsync(int detectiveId);
    Task LinkEvidenceToCaseAsync(int evidenceId, int caseId, int detectiveId);
    Task UnlinkEvidenceFromCaseAsync(int evidenceId, int caseId, int detectiveId);
}