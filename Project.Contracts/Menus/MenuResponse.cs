namespace Project.Contracts.Menus;

public sealed record MenuResponse(
    Guid Id,
    string Name,
    string Description,
    float? AverageRating,
    List<MenuSectionResponse> Sections,
    string HostId,
    List<string> DinnerIds,
    List<string> MenuReviewIds,
    DateTime CreatedDateTime,
    DateTime UpdatedDateTime);

public sealed record MenuSectionResponse(
    Guid Id,
    string Name,
    string Description,
    List<MenuItemResponse> Items);

public sealed record MenuItemResponse(
    Guid Id,
    string Name,
    string Description);