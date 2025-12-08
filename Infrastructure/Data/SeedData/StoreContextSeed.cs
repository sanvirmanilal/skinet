using System.Text.Json;
using Core.Entities;

namespace Infrastructure.Data;

public class StoreContextSeed
{
    public static async Task SeedAsync(StoreContext storeContext)
    {
        if (!storeContext.Products.Any())
        {
            var productsString = await File.ReadAllTextAsync("../Infrastructure/Data/SeedData/products.json");
            var products = JsonSerializer.Deserialize<List<Product>>(productsString);

            if (products == null)
            {
                return;
            }
            else
            {
                await storeContext.AddRangeAsync(products);
            }

            await storeContext.SaveChangesAsync();
        }
    }

}
