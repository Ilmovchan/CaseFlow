using CaseFlow.DAL.Models;

namespace CaseFlow.BLL.Interfaces.IAdmin;

public interface IAdminSuspectService
{
    Task<Suspect?> GetSuspectAsync(int suspectId);
    Task<List<Suspect>> GetSuspectsFromCaseAsync(int caseId);
    Task<List<Suspect>> GetSuspectsAsync();
    Task<List<Suspect>> GetPendingSuspectsAsync();
}