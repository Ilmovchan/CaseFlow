using CaseFlow.BLL.Dto.Detective;
using CaseFlow.BLL.Interfaces.IAdmin;
using Microsoft.AspNetCore.Mvc;

namespace CaseFlow.API.Controllers.Admin;

[ApiController]
[Route("admin/detectives")]
public class AdminDetectiveController(IAdminDetectiveService detectiveService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetDetectives() =>
        Ok(await detectiveService.GetDetectivesAsync());
    
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetDetective(int id)
    {
        var detective = await detectiveService.GetDetectiveAsync(id);

        return detective is null ? NotFound() : Ok(detective);
    }


    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> PostDetective(CreateDetectiveDto newDetective)
    {
        var detective = await detectiveService.CreateDetectiveAsync(newDetective);
        
        return CreatedAtAction(nameof(GetDetective), new {id = detective.Id}, detective);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> PutDetective(int id, UpdateDetectiveDto updateDetective)
    {
        await detectiveService.UpdateDetectiveAsync(id, updateDetective);
        
        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteDetective(int id)
    {
        await detectiveService.DeleteDetectiveAsync(id);

        return NoContent();
    }
}