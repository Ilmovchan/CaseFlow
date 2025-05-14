using CaseFlow.DAL.Interfaces;

namespace CaseFlow.BLL.Interfaces.Shared;

public interface ISubmittableService<TEntity>
    where TEntity : class,  IWorkflowEntity
{
    Task SubmitAsync(int id);
}