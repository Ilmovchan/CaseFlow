using CaseFlow.BLL.Exceptions;
using CaseFlow.BLL.Interfaces.Shared;
using CaseFlow.DAL.Data;
using CaseFlow.DAL.Enums;
using CaseFlow.DAL.Interfaces;

namespace CaseFlow.BLL.Services.AdminServices;

public class ApprovableService<TEntity> : IApprovableService<TEntity>
    where  TEntity : class, IWorkflowEntity
{
    private readonly DetectiveAgencyDbContext _context;
    
    public ApprovableService(DetectiveAgencyDbContext context) 
        => _context = context;
    
    public async Task ApproveAsync(int id)
    {
        var entity = await _context.Set<TEntity>().FindAsync(id);

        if (entity == null)
            throw new EntityNotFoundException(typeof(TEntity), id);
        
        entity.ApprovalStatus = ApprovalStatus.Approved;
        await _context.SaveChangesAsync();
    }

    public async Task RejectAsync(int id)
    {
        var entity = await _context.Set<TEntity>().FindAsync(id);

        if (entity == null)
            throw new EntityNotFoundException(typeof(TEntity), id);
        
        entity.ApprovalStatus = ApprovalStatus.Rejected;
        await _context.SaveChangesAsync();
    }
}