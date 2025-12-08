using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController(IRepository<Product> productRepository) : ControllerBase
{

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<Product>>> Get()
    {
        return Ok(await productRepository.GetAllAsync());
    }

    [HttpGet("{id:int}")] // api/products/2
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        var product = await productRepository.GetByIdAsync(id);
        return product == null
                ? NotFound()
                : Ok(product);
    }

    [HttpPost]
    public async Task<ActionResult<Product>> CreateProduct([FromBody] Product product)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        productRepository.Add(product);

        if (await productRepository.SaveChangesAsync())
        {
            return CreatedAtAction(nameof(GetProduct), new { id = product.Id });
        }

        return BadRequest("Failed to create the product. Please check the input data and try again.");
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> UpdateProduct(int id, Product product)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (id != product.Id)
        {
            return BadRequest("Route id does not match product id in body.");
        }

        productRepository.Update(product);

        if (await productRepository.SaveChangesAsync())
        {
            return Ok();
        }
        else
        {
            return NotFound();
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteProduct(int id)
    {
        var product = await productRepository.GetByIdAsync(id);

        if (product != null)
        {
            productRepository.Delete(product);
            await productRepository.SaveChangesAsync();
        }
        else
        {
            return NotFound();
        }

        return NoContent();
    }
}
