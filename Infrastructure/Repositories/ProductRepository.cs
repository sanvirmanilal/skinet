using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ProductRepository(StoreContext storeContext) : BaseRepository<Product>(storeContext), IProductRepository
{
    public async Task<IReadOnlyList<string>> GetBrandsAsync()
    {
        return await storeContext.Products
        .Select(x => x.Brand)
        .Distinct()
        .ToListAsync();
    }

    public async Task<IReadOnlyList<Product>> GetProductsAsync(string? brand, string? type, string? sort)
    {
        var products = storeContext.Products.AsQueryable();
        if (!string.IsNullOrWhiteSpace(brand))
        {
            products = products.Where(product => product.Brand == brand);
        }

        if (!string.IsNullOrWhiteSpace(type))
        {
            products = products.Where(product => product.Type == type);
        }

        if (!string.IsNullOrWhiteSpace(sort))
        {
            products = sort switch
            {
                "priceAsc" => products.OrderBy(product => product.Price),
                "priceDesc" => products.OrderByDescending(product => product.Price),
                "typeAsc" => products.OrderBy(product => product.Type),
                "typeDesc" => products.OrderByDescending(product => product.Type),
                "nameAsc" => products.OrderBy(product => product.Name),
                "nameDesc" => products.OrderByDescending(product => product.Name),
                "quantityAsc" => products.OrderBy(product => product.QuantityInStock),
                "quantityDesc" => products.OrderByDescending(product => product.QuantityInStock),
                _ => products.OrderBy(product => product.Name)
            };
        }

        return await products.ToListAsync();
    }

    public async Task<IReadOnlyList<string>> GetTypesAsync()
    {
        return await storeContext.Products
        .Select(x => x.Type)
        .Distinct()
        .ToListAsync();
    }
}
