namespace Project.Contracts.Menus;

public record GetAllMenusRequest(
    Guid? HostId,
    DateTime? CreatedDateTime,
    string? Name
);