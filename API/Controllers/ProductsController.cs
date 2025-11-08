using System.Net;
using Core.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController(StoreContext storeContext) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> Get()
    {
        return await storeContext.Products.ToListAsync();
    }

    [HttpGet("{id:int}")] // api/products/2
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        var product = await storeContext.Products.FindAsync(id);
        return product == null
                ? NotFound()
                : product;
    }

    /// <summary>
    /// Creates a new product in the store.
    /// </summary>
    /// <param name="product">The product to create.</param>
    /// <returns>The created product if successful; otherwise, a BadRequest result.</returns>
    [HttpPost]
    public async Task<ActionResult<Product>> CreateProduct([FromBody] Product product)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await storeContext.Products.AddAsync(product);

        var result = await storeContext.SaveChangesAsync();

        if (result > 0)
        {
            var productDto = new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price
            };
            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, productDto);
        }

        return BadRequest("Failed to create the product. Please check the input data and try again.");
    }
}
