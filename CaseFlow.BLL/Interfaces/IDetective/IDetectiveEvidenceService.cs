using CaseFlow.BLL.Dto.Evidence;
using CaseFlow.DAL.Models;

namespace CaseFlow.BLL.Interfaces.IDetective;

public interface IDetectiveEvidenceService
{
    Task<Evidence> CreateEvidenceAsync(int caseId, CreateEvidenceDto dto, int detectiveId);
    Task<Evidence> UpdateEvidenceAsync(int evidenceId, UpdateEvidenceDto dto, int detectiveId);
    Task DeleteEvidenceAsync(int evidenceId, int detectiveId);
    
    Task<List<Evidence>> GetAssignedEvidencesAsync(int detectiveId);
    Task<List<Evidence>> GetUnassignedEvidencesAsync(int detectiveId);
    Task<List<Evidence>> GetEvidencesAsync(int detectiveId);
    Task<List<Evidence>> GetDeclinedEvidencesAsync(int detectiveId);
    Task<List<Evidence>> GetApprovedEvidencesAsync(int detectiveId);
    Task<Evidence?> GetEvidenceAsync(int evidenceId, int detectiveId);
    Task<List<Evidence>> GetEvidencesFromCase(int caseId, int detectiveId);
    
    Task<List<Evidence>> GetPendingEvidencesAsync(int detectiveId);
    Task LinkEvidenceToCaseAsync(int evidenceId, int caseId, int detectiveId);
    Task UnlinkEvidenceFromCaseAsync(int evidenceId, int caseId, int detectiveId);
}