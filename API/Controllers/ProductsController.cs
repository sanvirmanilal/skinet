using System.Runtime.CompilerServices;
using Core.Entities;
using Infrastracture.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController(IDbContextFactory<StoreContext> contextFactory) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
    {
        using var dbContext = contextFactory.CreateDbContext();
        return await dbContext.Products.ToListAsync();
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        using var dbContext = contextFactory.CreateDbContext();
        var product = await dbContext.Products.FirstOrDefaultAsync(x=>x.Id.Equals(id));
        return product != null ? product : NotFound();
    }

    [HttpPost]
    public async Task<ActionResult<Product>> CreateProduct(Product product)
    {
        using var dbContext = contextFactory.CreateDbContext();
        dbContext.Products.Add(product);
        await dbContext.SaveChangesAsync();

        return product;        
    }

    [HttpPut("{id:int}")]    
    public async Task<ActionResult<Product>> UpdateProduct(int id, Product product)    
    {
        using var dbContext = contextFactory.CreateDbContext();
        dbContext.Products.Update(product);
        await dbContext.SaveChangesAsync();
        return product;
    }

    [HttpDelete("{id:int}")]    
    public async Task<ActionResult> DeleteProduct(int id)
    {
        using var dbContext = contextFactory.CreateDbContext();
        var product = await dbContext.Products.FindAsync(id);
        if (product is not null)
        {
            dbContext.Products.Remove(product);
            await dbContext.SaveChangesAsync();
            return Ok();
        }
        else 
        {
            return NotFound();
        }
    }
}