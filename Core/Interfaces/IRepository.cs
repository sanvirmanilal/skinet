using Core.Entities;

namespace Core.Interfaces;

public interface IRepository<T> where T : BaseEntity
{
    Task<T?> GetByIdAsync(int id);
    Task<IReadOnlyList<T>> GetAllAsync();
    void Delete(T entity);
    void Update(T entity);
    void Add(T entity);
    bool Exists(int id);
    Task<bool> SaveChangesAsync();

}
