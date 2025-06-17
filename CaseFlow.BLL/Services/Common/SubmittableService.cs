using CaseFlow.BLL.Exceptions;
using CaseFlow.BLL.Interfaces.Shared;
using CaseFlow.DAL.Data;
using CaseFlow.DAL.Enums;
using CaseFlow.DAL.Interfaces;

namespace CaseFlow.BLL.Services.Common;

public class SubmittableService<TEntity>(DetectiveAgencyDbContext context) : ISubmittableService<TEntity>
    where TEntity : class, IWorkflowEntity
{
    public async Task SubmitAsync(int id)
    {
        var entity = await context.Set<TEntity>().FindAsync(id);
        
        if (entity == null)
            throw new EntityNotFoundException(typeof(TEntity), id);

        entity.ApprovalStatus = ApprovalStatus.Pending;
        await context.SaveChangesAsync();
    }
}