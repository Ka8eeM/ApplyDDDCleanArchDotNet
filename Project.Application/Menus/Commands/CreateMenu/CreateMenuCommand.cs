using Project.Application.Messaging;
using Project.Domain.MenuAggregate;

namespace Project.Application.Menus.Commands.CreateMenu;

public sealed record CreateMenuCommand(
    string Name,
    string Description,
    double Average,
    int Rating,
    Guid HostId,
    List<MenuSectionCommand> Sections) : ICommand<CreateMenuCommandResponse>;

public sealed record MenuSectionCommand(
    string Name,
    string Description,
    List<MenuItemCommand> Items
    );

public sealed record MenuItemCommand(
    string Name,
    string Description);