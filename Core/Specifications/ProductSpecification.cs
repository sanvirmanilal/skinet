using Core.Entities;

namespace Core.Specifications;

public class ProductSpecification : BaseSpecification<Product>
{
    public ProductSpecification(ProductSpecParams productSpecParams)
        : base(x => (productSpecParams.Brands.Count == 0 || productSpecParams.Brands.Contains(x.Brand))
                        && (productSpecParams.Types.Count == 0 || productSpecParams.Types.Contains(x.Type)))
    {
        SetPageSize(productSpecParams.PageSize);
        SetPageNumber(productSpecParams.PageNumber);

        switch (productSpecParams.Sort)
        {
            case "priceAsc":
                {
                    SetOrderBy(x => x.Price);
                    break;
                }
            case "priceDesc":
                {
                    SetOrderByDesc(x => x.Price);
                    break;
                }
            default:
                {
                    SetOrderBy(x => x.Name);
                    break;
                }
        }
    }
}
