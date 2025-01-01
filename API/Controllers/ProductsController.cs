using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController(IGenericRepository<Product> productRepository) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts(string? brand, string? type, string? sort)
    {
        return Ok(await productRepository.ListAllAsync());
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        var product = await productRepository.GetByIdAsync(id);
        return product != null ? product : NotFound();
    }

    [HttpPost]
    public async Task<ActionResult<Product>> AddProduct(Product product)
    {
        await productRepository.AddAsync(product);
        if (await productRepository.SaveAllChangesAsync())
        {
            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
        }
        else
        {
            return BadRequest("Problem creating product");
        }
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<Product>> UpdateProduct(int id, Product product)
    {
        if (id.Equals(product.Id) && await productRepository.Exists(id))
        {
            productRepository.Update(product);
            if (await productRepository.SaveAllChangesAsync())
            {
                return NoContent();
            }
            else
            {
                return BadRequest("Problem updating product");
            }
        }

        return product;
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteProduct(int id)
    {
        if (await productRepository.Exists(id))
        {
            var product = await productRepository.GetByIdAsync(id);

            if (product is not null)
            {
                productRepository.Delete(product);
            }

            if (await productRepository.SaveAllChangesAsync())
            {
                return NoContent();
            }
            else
            {
                return BadRequest("Problem deleting product");
            }
        }
        else
        {
            return NotFound();
        }
    }

    [HttpGet("brands")]
    public async Task<ActionResult<IReadOnlyList<string>>> GetBrands()
    {
        return Ok();
    }

    [HttpGet("types")]
    public async Task<ActionResult<IReadOnlyList<string>>> GetTypes()
    {
        return Ok();
    }
}