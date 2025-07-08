using CaseFlow.BLL.Dto.Suspect;
using CaseFlow.DAL.Models;

namespace CaseFlow.BLL.Interfaces.IDetective;

public interface IDetectiveSuspectService
{
    Task<SuspectDto> CreateSuspectAsync(int caseId, CreateSuspectDto dto, int detectiveId);
    Task<SuspectDto> UpdateSuspectAsync(int suspectId, UpdateSuspectDto dto, int detectiveId);
    Task DeleteSuspectAsync(int suspectId, int detectiveId);

    Task<List<SuspectDto>> GetAssignedSuspectsAsync(int detectiveId);
    Task<List<SuspectDto>> GetUnassignedSuspectsAsync(int detectiveId);
    Task<List<SuspectDto>> GetSuspectsAsync(int detectiveId);
    Task<List<SuspectDto>> GetDeclinedSuspectsAsync(int detectiveId);
    Task<List<SuspectDto>> GetApprovedSuspectsAsync(int detectiveId);
    Task<SuspectDto?> GetSuspectAsync(int suspectId, int detectiveId);

    Task<List<SuspectDto>> GetPendingSuspectsAsync(int detectiveId);
    Task LinkSuspectToCaseAsync(int suspectId, int caseId, int detectiveId);
    Task UnlinkSuspectFromCaseAsync(int suspectId, int caseId, int detectiveId);
}