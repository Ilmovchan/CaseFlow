using CaseFlow.BLL.Dto.Case;
using CaseFlow.BLL.Interfaces.IAdmin;
using Microsoft.AspNetCore.Mvc;

namespace CaseFlow.API.Controllers.Admin;

[ApiController]
[Route("admin/cases")]
public class AdminCaseController(IAdminCaseService caseService) : AdminBaseController
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetCases() =>
        Ok(await caseService.GetCasesAsync());
    
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetCase(int id)
    {
        var caseEntity = await caseService.GetCaseAsync(id);

        return caseEntity is null ? NotFound() : Ok(caseEntity);
    }


    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> PostCase(CreateCaseDto newCase)
    {
        var caseEntity = await caseService.CreateCaseAsync(newCase);
        
        return CreatedAtAction(nameof(GetCase), new {id = caseEntity.Id}, caseEntity);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> PutCase(int id, UpdateCaseByAdminDto updateCase)
    {
        await caseService.UpdateCaseAsync(id, updateCase);
        
        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteCase(int id)
    {
        await caseService.DeleteCaseAsync(id);
        
        return NoContent();
    }
}