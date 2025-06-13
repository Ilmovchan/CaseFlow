using CaseFlow.BLL.Interfaces.IAdmin;
using Microsoft.AspNetCore.Mvc;

namespace CaseFlow.API.Controllers.Admin;

[ApiController]
[Route("admin/reports")]
public class AdminReportController(IAdminReportService service) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetReports() =>
        Ok(await service.GetReportsAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetReport(int id)
    {
        var item = await service.GetReportAsync(id);
        return item is null ? NotFound() : Ok(item);
    }

    [HttpGet("case")]
    public async Task<IActionResult> GetReportsFromCase(int caseId) =>
        Ok(await service.GetReportsFromCaseAsync(caseId));
    
    [HttpGet("pending")]
    public async Task<IActionResult> GetPendingReports() =>
        Ok(await service.GetPendingReportsAsync());
}