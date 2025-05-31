using CaseFlow.BLL.Dto.Client;
using CaseFlow.BLL.Interfaces.IAdmin;
using Microsoft.AspNetCore.Mvc;

namespace CaseFlow.API.Controllers.Admin;

[ApiController]
[Route("admin/clients")]
public class AdminClientController(IAdminClientService clientService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetClients()
    {
        var clients = await clientService.GetClientsAsync();

        return Ok(clients);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetClient(int id)
    {
        var client = await clientService.GetClientAsync(id);

        return client is null ? NotFound() : Ok(client);
    }


    [HttpPost]
    public async Task<IActionResult> PostClientAsync(CreateClientDto newClient)
    {
        var client = await clientService.CreateClientAsync(newClient);
        
        return CreatedAtAction(nameof(GetClient), new {id = client.Id}, client);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutClientAsync(int id, UpdateClientDto updateClient)
    {
        await clientService.UpdateClientAsync(id, updateClient);
        
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteClientAsync(int id)
    {
        await clientService.DeleteClientAsync(id);
        
        return NoContent();
    }
}