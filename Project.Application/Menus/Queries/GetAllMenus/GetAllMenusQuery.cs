using Project.Application.Messaging;

namespace Project.Application.Menus.Queries.GetAllMenus;

public sealed record GetAllMenusQuery() : ICachedQuery<List<GetAllMenusQueryResponse>>
{
    string ICachedQuery.CacheKey => $"get-all-menus";
    TimeSpan? ICachedQuery.Expiration => null;

    public Guid? HostId { get; set; }
    public DateTime? CreatedDateTime { get; set; }
    public string? Name { get; set; } = string.Empty;
}