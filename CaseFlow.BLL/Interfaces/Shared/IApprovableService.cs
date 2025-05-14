using CaseFlow.DAL.Interfaces;

namespace CaseFlow.BLL.Interfaces.Shared;

public interface IApprovableService<TEntity>
    where TEntity : class, IWorkflowEntity
{
    Task ApproveAsync(int id);
    Task RejectAsync(int id);
}
