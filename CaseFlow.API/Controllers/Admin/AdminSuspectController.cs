using CaseFlow.BLL.Interfaces.IAdmin;
using Microsoft.AspNetCore.Mvc;

namespace CaseFlow.API.Controllers.Admin;

[ApiController]
[Route("admin/suspects")]
public class AdminSuspectController(IAdminSuspectService service) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetSuspects() =>
        Ok(await service.GetSuspectsAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetSuspect(int id)
    {
        var item = await service.GetSuspectAsync(id);
        return item is null ? NotFound() : Ok(item);
    }

    [HttpGet("case")]
    public async Task<IActionResult> GetSuspectsFromCase(int caseId) =>
        Ok(await service.GetSuspectsFromCaseAsync(caseId));
    
    [HttpGet("pending")]
    public async Task<IActionResult> GetPendingSuspects() =>
        Ok(await service.GetPendingSuspectsAsync());
}