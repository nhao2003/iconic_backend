using Core.Entities;
using Core.Specifications.Params;

namespace Core.Specifications;

public class ProductSpecification : BaseSpecification<Product>
{
    public ProductSpecification(ProductSpecParams specParams) : base(x =>
        (string.IsNullOrEmpty(specParams.Search) || x.Name.ToLower().Contains(specParams.Search)) &&
        (specParams.Brands.Count == 0 || specParams.Brands.Contains(x.Brand)) &&
        (specParams.Types.Count == 0 || specParams.Types.Contains(x.Type))
    )
    {
        AddInclude("Variants.AttributeValues");
        AddInclude("Category.CategoryDescriptions");
        AddInclude("ProductCustomOptions.ProductCustomOptionValues");

        ApplyPaging(specParams.PageSize * (specParams.PageIndex - 1), specParams.PageSize);

        switch (specParams.Sort)
        {
            case "priceAsc":
                AddOrderBy(x => x.Price);
                break;
            case "priceDesc":
                AddOrderByDescending(x => x.Price);
                break;
            default:
                AddOrderBy(x => x.Name);
                break;
        }
    }

    public ProductSpecification(int id)
    {
        AddInclude(x => x.Category);

        AddInclude(x => x.Attributes);

        AddInclude(x => x.Variants);

        AddInclude(x => x.ProductCustomOptions);
    }
}
