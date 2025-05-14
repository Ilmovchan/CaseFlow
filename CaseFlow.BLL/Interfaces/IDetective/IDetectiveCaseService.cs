using CaseFlow.BLL.Dto.Case;
using CaseFlow.DAL.Models;

namespace CaseFlow.BLL.Interfaces.IDetective;

public interface IDetectiveCaseService
{
    Task<Case> UpdateCaseAsync(UpdateCaseByDetectiveDto dto, int detectiveId);
    
    Task<List<Case>> GetCasesAsync(int detectiveId);
    Task<Case?> GetCaseAsync(int caseId, int detectiveId);
}