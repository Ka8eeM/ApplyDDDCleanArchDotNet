using Project.Domain.Common.Primitives;
using Project.Domain.Common.ValueObjects;
using Project.Domain.DinnerAggregate.ValueObjects;
using Project.Domain.HostAggregate.ValueObjects;
using Project.Domain.MenuAggregate.Entities;
using Project.Domain.MenuAggregate.Events;
using Project.Domain.MenuAggregate.ValueObjects;
using Project.Domain.MenuReviewAggregate.ValueObjects;

namespace Project.Domain.MenuAggregate;

/// <summary>
/// this is a menu aggregate root for the menu feature
/// </summary>
public sealed class Menu : AggregateRoot<MenuId, Guid>
{

    private readonly List<MenuSection> _sections = new();
    private readonly List<DinnerId> _dinnerIds = new();
    private readonly List<MenuReviewId> _menuReviewsIds = new();

    private Menu()
    {

    }
    private Menu(
        MenuId id,
        string name,
        string description,
        AverageRating rating,
        HostId hostId,
        DateTime createdDateTime,
        DateTime updatedDateTime,
        List<MenuSection> sections) : base(id)
    {
        Name = name;
        Description = description;
        AverageRating = rating;
        HostId = hostId;
        CreatedDateTime = createdDateTime;
        UpdatedDateTime = updatedDateTime;
        _sections = sections;
    }


    public static Menu Create(
        string name,
        string description,
        double avgRate,
        int rating,
        Guid hostId,
        List<MenuSection> sections)
    {
        var menu = new Menu(
            MenuId.CreateUnique(Guid.NewGuid()),
            name,
            description,
            AverageRating.CreateNew(avgRate, rating),
            HostId.CreateUnique(hostId),
            DateTime.UtcNow,
            DateTime.UtcNow,
            sections);
        menu.AddDomainEvent(new MenuCreatedEvent(menu));
        return menu;
    }

    public void SetName(string name)
    {
        this.Name = name;
    }
    public void SetDescription(string description)
    {
        this.Description = description;
    }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public AverageRating AverageRating { get; private set; }
    public HostId HostId { get; private set; }
    public IReadOnlyList<MenuSection> Sections => _sections.AsReadOnly();
    public IReadOnlyList<DinnerId> DinnerIds => _dinnerIds.AsReadOnly();
    public IReadOnlyList<MenuReviewId> MenuReviewsIds => _menuReviewsIds.AsReadOnly();
    public DateTime CreatedDateTime { get; private set; }
    public DateTime UpdatedDateTime { get; private set; }

}
