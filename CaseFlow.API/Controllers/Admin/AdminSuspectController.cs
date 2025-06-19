using CaseFlow.BLL.Interfaces.IAdmin;
using Microsoft.AspNetCore.Mvc;

namespace CaseFlow.API.Controllers.Admin;

[ApiController]
[Route("admin/suspects")]
public class AdminSuspectController(IAdminSuspectService service) : AdminBaseController
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetSuspects() =>
        Ok(await service.GetSuspectsAsync());

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetSuspect(int id)
    {
        var item = await service.GetSuspectAsync(id);
        return item is null ? NotFound() : Ok(item);
    }

    [HttpGet("case/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetSuspectsFromCase(int id) =>
        Ok(await service.GetSuspectsFromCaseAsync(id));
    
    [HttpGet("pending")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetPendingSuspects() =>
        Ok(await service.GetPendingSuspectsAsync());
}