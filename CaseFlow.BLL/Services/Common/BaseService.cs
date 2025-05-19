using CaseFlow.BLL.Exceptions;
using CaseFlow.DAL.Data;
using Microsoft.EntityFrameworkCore;

namespace CaseFlow.BLL.Services.Common;

public abstract class BaseService<TEntity>(DetectiveAgencyDbContext context)
    where TEntity : class
{
    protected async Task<TEntity> GetByIdAsync(int id, string entityName)
    {
        var entity = await context.Set<TEntity>()
            .FindAsync(id) ?? throw new EntityNotFoundException(entityName, id);
        return entity;
    }

    protected Task<List<TEntity>> GetAllAsync()
    {
        return context.Set<TEntity>().ToListAsync();
    }

    protected async Task<TEntity> AddAsync(TEntity entity)
    {
        context.Set<TEntity>().Add(entity);
        await context.SaveChangesAsync();
        return entity;
    }

    protected async Task<TEntity> UpdateAsync(TEntity entity)
    {
        context.Set<TEntity>().Update(entity);
        await context.SaveChangesAsync();
        return entity;
    }

    protected async Task DeleteAsync(TEntity entity)
    {
        context.Set<TEntity>().Remove(entity);
        await context.SaveChangesAsync();
    }
} 