using Project.Application.Messaging;
using Project.Domain.MenuAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Menus.Commands.UpdateMenu;

public sealed record UpdateMenuCommand(
    Guid Id,
    string Name,
    string Description) : ICommand<bool>;