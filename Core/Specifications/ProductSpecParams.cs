namespace Core.Specifications;

public class ProductSpecParams
{
    private List<string> brands = [];
    private List<string> types = [];

    public List<string> Brands
    {
        get => brands;
        set
        {
            brands = value.SelectMany(x => x.Split(",", StringSplitOptions.RemoveEmptyEntries)).ToList();
        }
    }
    public List<string> Types
    {
        get => types;
        set
        {
            types = value.SelectMany(x => x.Split(",", StringSplitOptions.RemoveEmptyEntries)).ToList();
        }
    }

    public string? Sort { get; set; }

    public string? Search { get; set; }
    public int PageSize { get; set; } = 5;
    public int PageNumber { get; set; } = 0;
}
