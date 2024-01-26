using Project.Domain.MenuAggregate;

namespace Project.Infrastructure.Presistance.Specifications.MenuSpecifications;

public class GetMenuByIdIncludeSectionsSpecification : Specification<Menu>
{
    public GetMenuByIdIncludeSectionsSpecification(Guid menuId)
        : base(m => m.Id.Value == menuId)
    {
        AddInclude(s => s.Sections);
    }
}