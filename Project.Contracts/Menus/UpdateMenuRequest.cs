namespace Project.Contracts.Menus;

public sealed record UpdateMenuRequest(
    string name,
    string description);
