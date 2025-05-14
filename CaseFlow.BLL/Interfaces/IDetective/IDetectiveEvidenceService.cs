using CaseFlow.BLL.Dto.Evidence;
using CaseFlow.DAL.Models;

namespace CaseFlow.BLL.Interfaces.IDetective;

public interface IDetectiveEvidenceService
{
    Task<Evidence> CreateEvidenceAsync(int caseId, CreateEvidenceDto dto, int detectiveId);
    Task<Evidence> UpdateEvidenceAsync(UpdateEvidenceDto dto, int detectiveId);
    Task DeleteEvidenceAsync(int evidenceId, int detectiveId);
    
    Task<List<Evidence>> GetEvidencesAsync(int detectiveId);
    Task<List<Evidence>> GetRejectedEvidencesAsync(int detectiveId);
    Task<List<Evidence>> GetApprovedEvidencesAsync(int detectiveId);
    Task<Evidence?> GetEvidenceAsync(int evidenceId, int detectiveId);
    
    Task<List<Evidence>> GetSubmittedEvidencesAsync(int detectiveId);
    Task LinkEvidenceToCaseAsync(int evidenceId, int caseId, int detectiveId);
    Task UnlinkEvidenceFromCaseAsync(int evidenceId, int caseId, int detectiveId);
}