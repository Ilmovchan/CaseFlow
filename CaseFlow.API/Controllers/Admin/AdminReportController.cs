using CaseFlow.BLL.Interfaces.IAdmin;
using Microsoft.AspNetCore.Mvc;

namespace CaseFlow.API.Controllers.Admin;

[ApiController]
[Route("admin/reports")]
public class AdminReportController(IAdminReportService service) : AdminBaseController
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetReports() =>
        Ok(await service.GetReportsAsync());

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetReport(int id)
    {
        var item = await service.GetReportAsync(id);
        return item is null ? NotFound() : Ok(item);
    }

    [HttpGet("case/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetReportsFromCase(int id) =>
        Ok(await service.GetReportsFromCaseAsync(id));
    
    [HttpGet("pending")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetPendingReports() =>
        Ok(await service.GetPendingReportsAsync());
}