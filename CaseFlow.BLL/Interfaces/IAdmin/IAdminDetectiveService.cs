using CaseFlow.BLL.Dto.Detective;
using CaseFlow.DAL.Models;

namespace CaseFlow.BLL.Interfaces.IAdmin;

public interface IAdminDetectiveService
{
    Task<Detective> CreateDetectiveAsync(CreateDetectiveDto dto);
    Task<Detective> UpdateDetectiveAsync(int id, UpdateDetectiveDto dto);
    Task DeleteDetectiveAsync(int detectiveId);
    
    Task<Detective?> GetDetectiveAsync(int detectiveId);
    Task<List<Detective>> GetDetectivesAsync();
    Task<List<Detective>> GetUnassignedDetectivesAsync();
    
    Task<(Case, Detective)> AssignDetectiveAsync(int caseId, int detectiveId);
    Task DismissDetectiveAsync(int caseId);
}