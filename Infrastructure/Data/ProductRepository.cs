using Core.Entities;
using Core.Interfaces;
using Infrastracture.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class ProductRepository(StoreContext dbContext) : IProductRepository
{
    public void AddProduct(Product product)
    {
        dbContext.Products.Add(product);
    }

    public void DeleteProduct(Product product)
    {
        dbContext.Products.Remove(product);
    }

    public async Task<IReadOnlyList<string>> GetBrandsAsync()
    {
        return await dbContext.Products.Select(x => x.Brand)
        .Distinct()
        .ToListAsync();
    }

    public async Task<IReadOnlyList<string>> GetTypesAsync()
    {
        return await dbContext.Products.Select(x => x.Type)
        .Distinct()
        .ToListAsync();
    }

    public async Task<Product?> GetProductByIdAsync(int id)
    {
        return await dbContext.Products.FindAsync(id);
    }

    public async Task<IReadOnlyList<Product>> GetProductsAsync()
    {
        return await dbContext.Products.ToListAsync();
    }

    public bool ProductExists(int id)
    {
        return dbContext.Products.Any(x => x.Id.Equals(id));
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await dbContext.SaveChangesAsync() > 0;
    }

    public void UpdateProduct(Product product)
    {
        dbContext.Products.Update(product);
    }
}
