using CaseFlow.BLL.Interfaces.IDetective;
using Microsoft.AspNetCore.Mvc;

namespace CaseFlow.API.Controllers.Detective;

[ApiController]
[Route("detective/clients")]
public class DetectiveClientController(IDetectiveClientService clientService) : DetectiveBaseController
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetCases() =>
        Ok(await clientService.GetClientsAsync(DetectiveId));

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetCase(int id)
    {
        var client = await clientService.GetClientAsync(id, DetectiveId);
        
        return client is null ? NotFound() : Ok(client); 
    }
    
}