using Core.Entities;

namespace Core.Specifications;

public class BrandListSpecification : BaseSpecification<Product, string>
{
    public BrandListSpecification() : base(null)
    {
        SetSelect(x => x.Type);
        ApplyDistinct();
    }
}
