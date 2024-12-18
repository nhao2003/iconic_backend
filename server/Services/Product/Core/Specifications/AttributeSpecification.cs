using Core.Entities;
using Core.Specifications.Params;

namespace Core.Specifications;

public class AttributeSpecification : BaseSpecification<ProductAttribute>
{
    public AttributeSpecification(AttributeSpecParams specParams) : base(x =>
        (string.IsNullOrEmpty(specParams.Search) || x.AttributeName.ToLower().Contains(specParams.Search))
    )
    {
        AddInclude(x => x.AttributeOptions);

        ApplyPaging(specParams.PageSize * (specParams.PageIndex - 1), specParams.PageSize);

        switch (specParams.Sort)
        {
            case "codeAsc":
                AddOrderBy(x => x.AttributeCode);
                break;
            case "codeDesc":
                AddOrderByDescending(x => x.AttributeCode);
                break;
            default:
                AddOrderBy(x => x.AttributeCode);
                break;
        }
    }

    public AttributeSpecification(long id) : base(x => x.Id == id)
    {
        AddInclude(x => x.AttributeOptions);
    }
}