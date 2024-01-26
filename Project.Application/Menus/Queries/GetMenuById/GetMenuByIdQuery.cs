using Project.Application.Messaging;

namespace Project.Application.Menus.Queries.GetMenuById;

public sealed record GetMenuByIdQuery(Guid menuId) : ICachedQuery<GetMenuByIdQueryResponse>
{
    public string CacheKey => $"menu-by-id-{menuId}";

    public TimeSpan? Expiration => null;
}
