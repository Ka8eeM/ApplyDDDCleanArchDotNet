using MediatR;
using Project.Domain.MenuAggregate.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Menus.Commands.CreateMenu;

public sealed class MenuCreatedEventHandler : INotificationHandler<MenuCreatedEvent>
{


    public async Task Handle(MenuCreatedEvent notification, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        Console.WriteLine("This is a menu created event handler");
    }
}