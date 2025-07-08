using CaseFlow.BLL.Dto.Evidence;
using CaseFlow.BLL.Interfaces.IDetective;
using Microsoft.AspNetCore.Mvc;

namespace CaseFlow.API.Controllers.Detective;

[ApiController]
[Route("detective/evidences")]
public class DetectiveEvidenceController(IDetectiveEvidenceService evidenceService) : DetectiveBaseController
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetEvidences() =>
        Ok(await evidenceService.GetEvidencesAsync(DetectiveId));

    [HttpGet("all")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAllEvidences() =>
        Ok(await evidenceService.GetAllEvidencesAsync(DetectiveId));
    
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetEvidence(int id)
    {
        var evidence = await evidenceService.GetEvidenceAsync(id, DetectiveId);

        return evidence is null ? NotFound() : Ok(evidence);
    }


    [HttpPost("{caseId}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> PostEvidence(int caseId, CreateEvidenceDto newEvidence)
    {
        var evidence = await evidenceService.CreateEvidenceAsync(caseId, newEvidence, DetectiveId);
        
        return CreatedAtAction(nameof(GetEvidence), new {id = evidence.EvidenceId}, evidence);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> PutEvidence(int id, UpdateEvidenceDto updateEvidence)
    {
        await evidenceService.UpdateEvidenceAsync(id, updateEvidence, DetectiveId);
        
        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteEvidence(int id)
    {
        await evidenceService.DeleteEvidenceAsync(id, DetectiveId);
        
        return NoContent();
    }
    
    [HttpGet("case/{caseId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetEvidencesFromCase(int caseId) =>
        Ok(await evidenceService.GetEvidencesFromCase(caseId, DetectiveId));
    
    [HttpGet("assigned")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAssignedEvidences() =>
        Ok(await evidenceService.GetAssignedEvidencesAsync(DetectiveId));
    
    [HttpGet("unassigned")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetUnassignedEvidences() =>
        Ok(await evidenceService.GetUnassignedEvidencesAsync(DetectiveId));
    
    [HttpGet("approved")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetApprovedEvidences() =>
        Ok(await evidenceService.GetApprovedEvidencesAsync(DetectiveId));
    
    [HttpGet("declined")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetDeclinedEvidences() =>
        Ok(await evidenceService.GetDeclinedEvidencesAsync(DetectiveId));
    
    [HttpGet("pending")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetPendingEvidences() =>
        Ok(await evidenceService.GetPendingEvidencesAsync(DetectiveId));

    [HttpPut("{evidenceId}/link/{caseId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> LinkEvidenceToCase(int evidenceId, int caseId)
    {
        await evidenceService.LinkEvidenceToCaseAsync(evidenceId, caseId, DetectiveId);
        
        return NoContent();
    }

    [HttpPut("{evidenceId}/unlink/{caseId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UnlinkEvidenceFromCase(int evidenceId, int caseId)
    {
        await evidenceService.UnlinkEvidenceFromCaseAsync(evidenceId, caseId, DetectiveId);
        
        return NoContent();
    }
}