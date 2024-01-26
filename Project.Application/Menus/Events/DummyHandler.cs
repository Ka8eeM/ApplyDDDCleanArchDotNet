using MediatR;
using Project.Domain.MenuAggregate.Events;


namespace Project.Application.Menus.Events;

public class DummyHandler : INotificationHandler<MenuCreatedEvent>
{

    public Task Handle(MenuCreatedEvent notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
