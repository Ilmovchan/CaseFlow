using CaseFlow.BLL.Exceptions;
using CaseFlow.BLL.Interfaces.Shared;
using CaseFlow.DAL.Data;
using CaseFlow.DAL.Enums;
using CaseFlow.DAL.Interfaces;

namespace CaseFlow.BLL.Services.DetectiveServices;

public class SubmittableService<TEntity> : ISubmittableService<TEntity>
    where TEntity : class, IWorkflowEntity
{
    private readonly DetectiveAgencyDbContext _context;
    
    public SubmittableService(DetectiveAgencyDbContext context) 
        => _context = context;
    
    public async Task SubmitAsync(int id)
    {
        var entity = await _context.Set<TEntity>().FindAsync(id);
        
        if (entity == null)
            throw new EntityNotFoundException(typeof(TEntity), id);

        entity.ApprovalStatus = ApprovalStatus.Submitted;
        await _context.SaveChangesAsync();
    }
}