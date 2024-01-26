using Project.Domain.Common.Primitives;
using Project.Domain.MenuAggregate.ValueObjects;

namespace Project.Domain.MenuAggregate.Entities;

public sealed class MenuItem : Entity<MenuItemId>
{
    public string Name { get; private set; }
    public string Description { get; private set; }

    private MenuItem()
    {

    }
    private MenuItem(MenuItemId id, string name, string description) : base(id)
    {
        Name = name;
        Description = description;
    }

    public static MenuItem Create(string name, string Description)
    {
        return new MenuItem(MenuItemId.CreateUnique(Guid.NewGuid()), name, Description);
    }
}
