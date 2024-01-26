using Project.Domain.MenuAggregate;
using System.Linq.Expressions;

namespace Project.Infrastructure.Presistance.Specifications.MenuSpecifications;

public class GetAllMenusSpecifications : Specification<Menu>
{
    public GetAllMenusSpecifications(
        string? Name,
        Guid? HostId,
        DateTime? CreatedDateTime
        ) : base(menu =>
                (string.IsNullOrEmpty(Name) || menu.Name == Name) &&
                (HostId == null || menu.HostId.Value == HostId) &&
                (CreatedDateTime == null || menu.CreatedDateTime == CreatedDateTime))
    {
        IsSplitQuery = true;
        AddOrderBy(order => order.CreatedDateTime);
    }
}