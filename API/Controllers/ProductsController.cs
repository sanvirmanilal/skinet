using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController(IProductRepository productRepository) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts(string? brand, string? type)
    {
        return Ok(await productRepository.GetProductsAsync(brand, type));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        var product = await productRepository.GetProductByIdAsync(id);
        return product != null ? product : NotFound();
    }

    [HttpPost]
    public async Task<ActionResult<Product>> AddProduct(Product product)
    {
        productRepository.AddProduct(product);
        if (await productRepository.SaveChangesAsync())
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
        if (id.Equals(product.Id) && productRepository.ProductExists(id))
        {
            productRepository.UpdateProduct(product);
            if (await productRepository.SaveChangesAsync())
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
        if (productRepository.ProductExists(id))
        {
            var product = await productRepository.GetProductByIdAsync(id);
            productRepository.DeleteProduct(product);
             if (await productRepository.SaveChangesAsync())
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
        return Ok(await productRepository.GetBrandsAsync());
    }
    
    [HttpGet("types")]
    public async Task<ActionResult<IReadOnlyList<string>>> GetTypes()
    {
        return Ok(await productRepository.GetTypesAsync());
    }
}