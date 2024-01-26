namespace Project.Application.Menus.Queries.GetAllMenus;

public sealed record GetAllMenusQueryResponse(
    Guid menuId,
    string Name,
    string Description,
    double Average,
    int Rating,
    Guid HostId);