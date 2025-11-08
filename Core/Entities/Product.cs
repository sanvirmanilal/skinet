using System.ComponentModel.DataAnnotations;

namespace Core.Entities;

public class Product : BaseEntity
{
    public required string Brand { get; set; }
    public required string Description { get; set; }
    public required string Name { get; set; }
    public required string PictureUrl { get; set; }

    [DataType("decimal(18,2)")]
    public decimal Price { get; set; }
    public int QuantityInStock { get; set; }
    public required string Type { get; set; }
}
