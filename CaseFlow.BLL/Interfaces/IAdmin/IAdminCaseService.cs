using CaseFlow.BLL.Dto.Case;
using CaseFlow.DAL.Models;

namespace CaseFlow.BLL.Interfaces.IAdmin;

public interface IAdminCaseService
{
    Task<Case> CreateCaseAsync(CreateCaseDto dto);
    Task<Case> UpdateCaseAsync(int id, UpdateCaseByAdminDto dto);
    Task DeleteCaseAsync(int caseId);
    
    Task<Case?> GetCaseAsync(int caseId);
    Task<List<Case>> GetCasesAsync();
}