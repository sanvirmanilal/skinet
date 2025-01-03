using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class ProductsController(IGenericRepository<Product> repository) : BaseController
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts([FromQuery] ProductSpecParams productSpecParams)
    {
        var spec = new ProductSpecification(productSpecParams);
        return Ok(await CreatePagedResult(repository, spec, productSpecParams.PageNumber, productSpecParams.PageSize));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        var product = await repository.GetByIdAsync(id);
        return product != null ? product : NotFound();
    }

    [HttpPost]
    public async Task<ActionResult<Product>> AddProduct(Product product)
    {
        await repository.AddAsync(product);
        if (await repository.SaveAllChangesAsync())
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
        if (id.Equals(product.Id) && await repository.Exists(id))
        {
            repository.Update(product);
            if (await repository.SaveAllChangesAsync())
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
        if (await repository.Exists(id))
        {
            var product = await repository.GetByIdAsync(id);

            if (product is not null)
            {
                repository.Delete(product);
            }

            if (await repository.SaveAllChangesAsync())
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
        var spec = new BrandListSpecification();

        return Ok(await repository.ListAsync(spec));
    }


    [HttpGet("types")]
    public async Task<ActionResult<IReadOnlyList<string>>> GetTypes()
    {
        var spec = new TypeListSpecification();
        return Ok(await repository.ListAsync(spec));
    }
}