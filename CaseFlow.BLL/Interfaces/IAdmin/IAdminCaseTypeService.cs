using CaseFlow.BLL.Dto.CaseType;
using CaseFlow.DAL.Models;

namespace CaseFlow.BLL.Interfaces.IAdmin;

public interface IAdminCaseTypeService
{
    Task<CaseType> CreateCaseTypeAsync(CreateCaseTypeDto dto);
    Task<CaseType> UpdateCaseTypeAsync(UpdateCaseTypeDto dto);
    Task DeleteCaseTypeAsync(int caseTypeId);
    
    Task<List<CaseType>> GetCaseTypesAsync();
}