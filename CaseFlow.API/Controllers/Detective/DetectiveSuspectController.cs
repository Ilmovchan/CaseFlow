using CaseFlow.BLL.Dto.Suspect;
using CaseFlow.BLL.Interfaces.IDetective;
using Microsoft.AspNetCore.Mvc;

namespace CaseFlow.API.Controllers.Detective;

[ApiController]
[Route("detective/suspects")]
public class DetectiveSuspectController(IDetectiveSuspectService suspectService) : DetectiveBaseController
{
    [HttpPost("{caseId}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateSuspect(int caseId, CreateSuspectDto newSuspect)
    {
        var suspect = await suspectService.CreateSuspectAsync(caseId, newSuspect, DetectiveId);
        return CreatedAtAction(nameof(GetSuspect), new {id = suspect.Id}, suspect);
    }

    [HttpPut("{caseId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateSuspect(int id, UpdateSuspectDto updateSuspect)
    {
        await suspectService.UpdateSuspectAsync(id, updateSuspect, DetectiveId);
        return NoContent();
    }

    [HttpDelete("{caseId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteSuspect(int id)
    {
        await suspectService.DeleteSuspectAsync(id, DetectiveId);
        return NoContent();
    }

    [HttpGet("assigned")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAssignedSuspects() =>
        Ok(await suspectService.GetAssignedSuspectsAsync(DetectiveId));
    
    [HttpGet("unassigned")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetUnassignedSuspects() =>
        Ok(await suspectService.GetUnassignedSuspectsAsync(DetectiveId));

    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetSuspects() =>
        Ok(await suspectService.GetSuspectsAsync(DetectiveId));

    
    [HttpGet("declined")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetDeclinedSuspects() =>
        Ok(await suspectService.GetDeclinedSuspectsAsync(DetectiveId));
    
    [HttpGet("approved")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetApprovedSuspects() =>
        Ok(await suspectService.GetApprovedSuspectsAsync(DetectiveId));
    
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetSuspect(int id) =>
        Ok(await suspectService.GetSuspectAsync(id, DetectiveId));
    
    
    [HttpGet("pending")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetPendingSuspects() =>
        Ok(await suspectService.GetPendingSuspectsAsync(DetectiveId));

    [HttpPut("{suspectId}/link/{caseId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task LinkSuspectToCase(int id, int caseId)
    {
        await suspectService.LinkSuspectToCaseAsync(id, caseId, DetectiveId);
        NoContent();
    }

    [HttpPut("{suspectId}/unlink/{caseId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task UnlinkSuspectFromCase(int id, int caseId)
    {
        await suspectService.UnlinkSuspectFromCaseAsync(id, caseId, DetectiveId);
        NoContent();
    }
}