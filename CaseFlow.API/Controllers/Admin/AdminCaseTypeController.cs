using CaseFlow.BLL.Dto.CaseType;
using CaseFlow.BLL.Interfaces.IAdmin;
using Microsoft.AspNetCore.Mvc;

namespace CaseFlow.API.Controllers.Admin;

[ApiController]
[Route("admin/caseTypes")]
public class AdminCaseTypeController(IAdminCaseTypeService caseTypeService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetCaseTypes() =>
        Ok(await caseTypeService.GetCaseTypesAsync());

    [HttpPost]
    public async Task<IActionResult> PostCaseType(CreateCaseTypeDto newCaseType)
    {
        await caseTypeService.CreateCaseTypeAsync(newCaseType);
        
        return Created();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutCaseType(int id, UpdateCaseTypeDto updateCaseType)
    {
        await caseTypeService.UpdateCaseTypeAsync(id, updateCaseType);
        
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCaseType(int id)
    {
        await caseTypeService.DeleteCaseTypeAsync(id);
        
        return NoContent();
    }
}