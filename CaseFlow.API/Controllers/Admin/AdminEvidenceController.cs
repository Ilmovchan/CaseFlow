using CaseFlow.BLL.Interfaces.IAdmin;
using Microsoft.AspNetCore.Mvc;

namespace CaseFlow.API.Controllers.Admin;

[ApiController]
[Route("admin/evidences")]
public class AdminEvidenceController(IAdminEvidenceService service) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetEvidences() =>
        Ok(await service.GetEvidencesAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetEvidence(int id)
    {
        var item = await service.GetEvidenceAsync(id);
        return item is null ? NotFound() : Ok(item);
    }

    [HttpGet("case")]
    public async Task<IActionResult> GetEvidencesFromCase(int caseId) =>
        Ok(await service.GetEvidencesFromCaseAsync(caseId));
    
    [HttpGet("pending")]
    public async Task<IActionResult> GetPendingEvidences() =>
        Ok(await service.GetPendingEvidencesAsync());

}