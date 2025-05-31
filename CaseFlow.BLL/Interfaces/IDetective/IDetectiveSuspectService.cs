using CaseFlow.BLL.Dto.Suspect;
using CaseFlow.DAL.Models;

namespace CaseFlow.BLL.Interfaces.IDetective;

public interface IDetectiveSuspectService
{
    Task<Suspect> CreateSuspectAsync(int caseId, CreateSuspectDto dto, int detectiveId);
    Task<Suspect> UpdateSuspectAsync(int suspectId, UpdateSuspectDto dto, int detectiveId);
    Task DeleteSuspectAsync(int suspectId, int detectiveId);

    Task<List<Suspect>> GetAssignedSuspectsAsync(int detectiveId);
    Task<List<Suspect>> GetUnassignedSuspectsAsync(int detectiveId);
    Task<List<Suspect>> GetAllSuspectsAsync(int detectiveId);
    Task<List<Suspect>> GetRejectedSuspectsAsync(int detectiveId);
    Task<List<Suspect>> GetApprovedSuspectsAsync(int detectiveId);
    Task<Suspect?> GetSuspectAsync(int suspectId, int detectiveId);

    Task<List<Suspect>> GetSubmittedSuspectsAsync(int detectiveId);
    Task LinkSuspectToCaseAsync(int suspectId, int caseId, int detectiveId);
    Task UnlinkSuspectFromCaseAsync(int suspectId, int caseId, int detectiveId);
}