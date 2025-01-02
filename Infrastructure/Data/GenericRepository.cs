using Core.Entities;
using Core.Interfaces;
using Infrastracture.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class GenericRepository<T>(StoreContext storeContext) : IGenericRepository<T> where T : BaseEntity
{
    public async Task AddAsync(T entity)
    {
        await storeContext.Set<T>().AddAsync(entity);
    }

    public void Delete(T entity)
    {
        storeContext.Set<T>().Remove(entity);
    }

    public async Task<bool> Exists(int id)
    {
        return await storeContext.Set<T>().AnyAsync(x => x.Id.Equals(id));
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        return await storeContext.Set<T>().FindAsync(id);
    }

    public async Task<T?> GetEntityWithSpec(ISpecification<T> specification)
    {
        return await ApplySpecification(specification).FirstOrDefaultAsync();
    }

    public async Task<TResult?> GetEntityWithSpec<TResult>(ISpecification<T, TResult> specification)
    {
        return await ApplySpecification(specification).FirstOrDefaultAsync();
    }

    public async Task<IReadOnlyList<T>> ListAllAsync()
    {
        return await storeContext.Set<T>().ToListAsync();
    }

    public async Task<IReadOnlyList<T>> ListAsync(ISpecification<T> specification)
    {
        return await ApplySpecification(specification).ToListAsync();
    }

    public async Task<IReadOnlyList<TResult>> ListAsync<TResult>(ISpecification<T, TResult> specification)
    {
        return await ApplySpecification(specification).ToListAsync();
    }

    public async Task<IReadOnlyList<TResult>> ListDistinctAsync<TResult>(ISpecification<T, TResult> specification)
    {
        return await ApplySpecification(specification).ToListAsync();
    }

    public async Task<bool> SaveAllChangesAsync()
    {
        return await storeContext.SaveChangesAsync() > 0;
    }

    public void Update(T entity)
    {
        storeContext.Set<T>().Update(entity);
    }

    private IQueryable<T> ApplySpecification(ISpecification<T> specification)
    {
        var entities = storeContext.Set<T>();
        return SpecificationEvaluator<T>.GetQuery(entities, specification);
    }

    private IQueryable<TResult> ApplySpecification<TResult>(ISpecification<T, TResult> specification)
    {
        var entities = storeContext.Set<T>();
        return SpecificationEvaluator<T>.GetQuery(entities, specification);
    }
}
