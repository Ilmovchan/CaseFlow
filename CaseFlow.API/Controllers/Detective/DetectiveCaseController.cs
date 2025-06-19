using CaseFlow.BLL.Dto.Case;
using CaseFlow.BLL.Interfaces.IDetective;
using Microsoft.AspNetCore.Mvc;

namespace CaseFlow.API.Controllers.Detective;

[ApiController]
[Route("detective/cases")]
public class DetectiveCaseController(IDetectiveCaseService caseService) : DetectiveBaseController
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetCases() =>
        Ok(await caseService.GetCasesAsync(DetectiveId));
    
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetCase(int id)
    {
        var caseEntity = await caseService.GetCaseAsync(id, DetectiveId);

        return caseEntity is null ? NotFound() : Ok(caseEntity);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> PutCase(int id, UpdateCaseByDetectiveDto updateCase)
    {
        await caseService.UpdateCaseAsync(id, updateCase, DetectiveId);
        
        return NoContent();
    }
}