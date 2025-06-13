using CaseFlow.BLL.Dto.Case;
using CaseFlow.BLL.Interfaces.IAdmin;
using Microsoft.AspNetCore.Mvc;

namespace CaseFlow.API.Controllers.Admin;

[ApiController]
[Route("admin/cases")]
public class AdminCaseController(IAdminCaseService caseService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetCases() =>
        Ok(await caseService.GetCasesAsync());
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetCase(int id)
    {
        var caseEntity = await caseService.GetCaseAsync(id);

        return caseEntity is null ? NotFound() : Ok(caseEntity);
    }


    [HttpPost]
    public async Task<IActionResult> PostCase(CreateCaseDto newCase)
    {
        var caseEntity = await caseService.CreateCaseAsync(newCase);
        
        return CreatedAtAction(nameof(GetCase), new {id = caseEntity.Id}, caseEntity);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutCase(int id, UpdateCaseByAdminDto updateCase)
    {
        await caseService.UpdateCaseAsync(id, updateCase);
        
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCase(int id)
    {
        await caseService.DeleteCaseAsync(id);
        
        return NoContent();
    }
}