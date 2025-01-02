using Core.Entities;

namespace Core.Specifications;

public class TypeListSpecification : BaseSpecification<Product, string>
{
    public TypeListSpecification() : base(null)
    {
        SetSelect(x => x.Brand);
        ApplyDistinct();
    }
}

