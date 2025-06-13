using CaseFlow.BLL.Dto.Client;
using CaseFlow.BLL.Interfaces.IAdmin;
using Microsoft.AspNetCore.Mvc;

namespace CaseFlow.API.Controllers.Admin;

[ApiController]
[Route("admin/clients")]
public class AdminClientController(IAdminClientService clientService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetClients() =>
        Ok(await clientService.GetClientsAsync());
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetClient(int id)
    {
        var client = await clientService.GetClientAsync(id);

        return client is null ? NotFound() : Ok(client);
    }


    [HttpPost]
    public async Task<IActionResult> PostClient(CreateClientDto newClient)
    {
        var client = await clientService.CreateClientAsync(newClient);
        
        return CreatedAtAction(nameof(GetClient), new {id = client.Id}, client);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutClient(int id, UpdateClientDto updateClient)
    {
        await clientService.UpdateClientAsync(id, updateClient);
        
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteClient(int id)
    {
        await clientService.DeleteClientAsync(id);
        
        return NoContent();
    }
}