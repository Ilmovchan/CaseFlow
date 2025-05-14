using CaseFlow.BLL.Dto.Client;
using CaseFlow.DAL.Models;

namespace CaseFlow.BLL.Interfaces.IAdmin;

public interface IAdminClientService
{
    Task<Client> AddClientAsync(CreateClientDto dto);
    Task<Client> UpdateClientAsync(UpdateClientDto dto);
    Task DeleteClientAsync(int clientId);
    
    Task<Client?> GetClientAsync(int clientId);
    Task<List<Client>> GetClientsAsync();
}