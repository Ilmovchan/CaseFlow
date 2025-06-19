using CaseFlow.BLL.Dto.Report;
using CaseFlow.BLL.Interfaces.IDetective;
using Microsoft.AspNetCore.Mvc;

namespace CaseFlow.API.Controllers.Detective;

[ApiController]
[Route("detective/reports")]
public class DetectiveReportController(IDetectiveReportService reportService) : DetectiveBaseController
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetReports() =>
        Ok(await reportService.GetReportsAsync(DetectiveId));
    
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult?> GetReport(int reportId) => 
        Ok(await reportService.GetReportAsync(reportId, DetectiveId));

    [HttpPost("{caseId}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateReport(int caseId, CreateReportDto newReport)
    {
        var report = await reportService.CreateReportAsync(caseId, newReport, DetectiveId);
        return CreatedAtAction(nameof(GetReport), new {id = report.Id}, report);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateReport(int id, UpdateReportDto updateReport)
    {
        await reportService.UpdateReportAsync(id, updateReport, DetectiveId);
        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteReport(int id)
    {
        await reportService.DeleteReportAsync(id, DetectiveId);
        return NoContent();
    }

    [HttpGet("pending")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetPendingReports() =>
        Ok(await reportService.GetPendingReportsAsync(DetectiveId));
    
    [HttpGet("declined")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetDeclinedReports() =>
        Ok(await reportService.GetDeclinedReportsAsync(DetectiveId));
    
    [HttpGet("approved")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetApprovedReports() =>
        Ok(await reportService.GetApprovedReportsAsync(DetectiveId));
    
}