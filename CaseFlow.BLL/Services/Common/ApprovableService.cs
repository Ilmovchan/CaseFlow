using CaseFlow.BLL.Exceptions;
using CaseFlow.BLL.Interfaces.Shared;
using CaseFlow.DAL.Data;
using CaseFlow.DAL.Enums;
using CaseFlow.DAL.Interfaces;

namespace CaseFlow.BLL.Services.Common;

public class ApprovableService<TEntity>(DetectiveAgencyDbContext context) : IApprovableService<TEntity>
    where TEntity : class, IWorkflowEntity
{
    public async Task ApproveAsync(int id)
    {
        var entity = await context.Set<TEntity>().FindAsync(id);

        if (entity == null)
            throw new EntityNotFoundException(typeof(TEntity), id);
        
        entity.ApprovalStatus = ApprovalStatus.Approved;
        await context.SaveChangesAsync();
    }

    public async Task RejectAsync(int id)
    {
        var entity = await context.Set<TEntity>().FindAsync(id);

        if (entity == null)
            throw new EntityNotFoundException(typeof(TEntity), id);
        
        entity.ApprovalStatus = ApprovalStatus.Rejected;
        await context.SaveChangesAsync();
    }
}