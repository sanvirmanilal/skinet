using System.Text.Json;
using Core.Entities;
using Infrastracture.Data;

namespace Infrastructure.Data.SeedData;

public class StoreContextSeed
{
    public static async Task SeedAsync(StoreContext storeContext)
    {
        if (!storeContext.Products.Any())
        {
            var textProducts = await File.ReadAllTextAsync("../Infrastructure/Data/SeedData/products.json");
            var products = JsonSerializer.Deserialize<IEnumerable<Product>>(textProducts);
            if (products != null)
            {
                await storeContext.AddRangeAsync(products);
            }
        }
    }
}