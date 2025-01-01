using System.Text.Json;
using Core.Entities;
namespace Infrastructure.Data.SeedData;

public class StoreContextSeed
{
    public static async Task<List<Product>> GetSeedDataAsync()
    {
        var textProducts = await File.ReadAllTextAsync("../Infrastructure/Data/SeedData/products.json");
        var products = JsonSerializer.Deserialize<List<Product>>(textProducts);
        return products;
    }
}