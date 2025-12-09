using Core.Entities;

namespace Core.Interfaces;

public interface IProductRepository : IRepository<Product>
{
    Task<IReadOnlyList<string>> GetBrandsAsync();
    Task<IReadOnlyList<Product>> GetProductsAsync(string? brand, string? type, string? sort);
    Task<IReadOnlyList<string>> GetTypesAsync();
}
