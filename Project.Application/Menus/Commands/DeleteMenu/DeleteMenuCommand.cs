using Project.Application.Messaging;
using Project.Domain.MenuAggregate;

namespace Project.Application.Menus.Commands.DeleteMenu;

public sealed record DeleteMenuCommand(Guid id) : ICommand;
