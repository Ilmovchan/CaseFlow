using CaseFlow.DAL.Models;

namespace CaseFlow.BLL.Interfaces.IDetective;

public interface IDetectiveClientService
{
    Task<List<Client>> GetClientsAsync(int detectiveId);
    Task<Client?> GetClientAsync(int clientId, int detectiveId);

}