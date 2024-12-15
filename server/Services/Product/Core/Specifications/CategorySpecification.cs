using Core.Entities;
using Core.Specifications.Params;

namespace Core.Specifications;

public class CategorySpecification : BaseSpecification<Category>
{
    public CategorySpecification(CategorySpecParams specParams) : base(x =>
        (string.IsNullOrEmpty(specParams.Search) || x.Slug.ToLower().Contains(specParams.Search))
    )
    {
        AddInclude(x => x.CategoryDescriptions);

        ApplyPaging(specParams.PageSize * (specParams.PageIndex - 1), specParams.PageSize);

        switch (specParams.Sort)
        {
            case "slugAsc":
                AddOrderBy(x => x.Slug);
                break;
            case "slugDesc":
                AddOrderByDescending(x => x.Slug);
                break;
            default:
                AddOrderBy(x => x.Slug);
                break;
        }
    }
}