using CaseFlow.DAL.Models;

namespace CaseFlow.BLL.Interfaces.IAdmin;

public interface IAdminEvidenceService
{
    Task<Evidence?> GetEvidenceAsync(int evidenceId);
    Task<List<Evidence>> GetEvidencesFromCaseAsync(int caseId);
    Task<List<Evidence>> GetEvidencesAsync();
    Task<List<Evidence>> GetPendingEvidencesAsync();
}