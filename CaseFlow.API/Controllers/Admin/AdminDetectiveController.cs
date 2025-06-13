using CaseFlow.BLL.Dto.Detective;
using CaseFlow.BLL.Interfaces.IAdmin;
using Microsoft.AspNetCore.Mvc;

namespace CaseFlow.API.Controllers.Admin;

[ApiController]
[Route("admin/detectives")]
public class AdminDetectiveController(IAdminDetectiveService detectiveService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetDetectives() =>
        Ok(await detectiveService.GetDetectivesAsync());
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetDetective(int id)
    {
        var detective = await detectiveService.GetDetectiveAsync(id);

        return detective is null ? NotFound() : Ok(detective);
    }


    [HttpPost]
    public async Task<IActionResult> PostDetective(CreateDetectiveDto newDetective)
    {
        var detective = await detectiveService.CreateDetectiveAsync(newDetective);
        
        return CreatedAtAction(nameof(GetDetective), new {id = detective.Id}, detective);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutDetective(int id, UpdateDetectiveDto updateDetective)
    {
        await detectiveService.UpdateDetectiveAsync(id, updateDetective);
        
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDetective(int id)
    {
        await detectiveService.DeleteDetectiveAsync(id);

        return NoContent();
    }
}