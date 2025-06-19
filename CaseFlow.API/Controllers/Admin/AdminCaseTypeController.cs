using CaseFlow.BLL.Dto.CaseType;
using CaseFlow.BLL.Interfaces.IAdmin;
using Microsoft.AspNetCore.Mvc;

namespace CaseFlow.API.Controllers.Admin;

[ApiController]
[Route("admin/caseTypes")]
public class AdminCaseTypeController(IAdminCaseTypeService caseTypeService) : AdminBaseController
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetCaseTypes() =>
        Ok(await caseTypeService.GetCaseTypesAsync());

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> PostCaseType(CreateCaseTypeDto newCaseType)
    {
        await caseTypeService.CreateCaseTypeAsync(newCaseType);
        
        return Created();
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> PutCaseType(int id, UpdateCaseTypeDto updateCaseType)
    {
        await caseTypeService.UpdateCaseTypeAsync(id, updateCaseType);
        
        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteCaseType(int id)
    {
        await caseTypeService.DeleteCaseTypeAsync(id);
        
        return NoContent();
    }
}