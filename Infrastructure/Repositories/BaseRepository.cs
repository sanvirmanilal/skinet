using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class BaseRepository<T>(DbContext dbContext) : IRepository<T> where T : BaseEntity
{
    public void Add(T entity)
    {
        dbContext.Add(entity);
    }

    public void Delete(T entity)
    {
        dbContext.Remove(entity);
    }

    public bool Exists(int id)
    {
        return dbContext.Set<T>().Any(x => x.Id == id);
    }

    public async Task<IReadOnlyList<T>> GetAllAsync()
    {
        return await dbContext.Set<T>().ToListAsync();
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        return await dbContext.FindAsync<T>(id);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await dbContext.SaveChangesAsync() > 0;
    }

    public void Update(T entity)
    {
        dbContext.Update(entity);
    }
}