namespace Project.Application.Menus.Queries;

public sealed record GetMenuByIdQueryResponse(
    Guid menuId,
    string Name,
    string Description,
    double Average,
    int Rating,
    Guid HostId);
