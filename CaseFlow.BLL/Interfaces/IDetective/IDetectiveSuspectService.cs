using CaseFlow.BLL.Dto.Suspect;
using CaseFlow.DAL.Models;

namespace CaseFlow.BLL.Interfaces.IDetective;

public interface IDetectiveSuspectService
{
    Task<Suspect> CreateSuspectAsync(int caseId, CreateSuspectDto dto, int detectiveId);
    Task<Suspect> UpdateSuspectAsync(UpdateSuspectDto dto, int detectiveId);
    Task DeleteSuspectAsync(int suspectId, int detectiveId);
    
    Task<List<Suspect>> GetSuspectsAsync(int detectiveId);
    Task<List<Suspect>> GetUnassignedSuspectsAsync(int detectiveId);
    Task<Suspect?> GetSuspectAsync(int suspectId, int detectiveId);
    
    Task LinkSuspectToCaseAsync(int suspectId, int caseId, int detectiveId);
    Task UnlinkSuspectFromCaseAsync(int suspectId, int caseId, int detectiveId);
}