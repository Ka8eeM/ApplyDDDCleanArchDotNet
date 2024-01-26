using MediatR;
using Project.Application.Common.Interfaces.Caching;
using Project.Domain.MenuAggregate.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Menus;

internal class CacheInvalidationMenuHandler
    : INotificationHandler<MenuCreatedEvent>
    , INotificationHandler<MenuUpdatedEvent>
    , INotificationHandler<MenuDeletedEvent>
{

    private readonly ICacheService _cacheService;

    public CacheInvalidationMenuHandler(ICacheService cacheService)
    {
        _cacheService = cacheService;
    }

    public Task Handle(MenuCreatedEvent notification, CancellationToken cancellationToken)
    {
        return HandleInternal(notification.menu.Id.Value, cancellationToken);
    }

    public Task Handle(MenuUpdatedEvent notification, CancellationToken cancellationToken)
    {
        return HandleInternal(notification.menu.Id.Value, cancellationToken);
    }

    public Task Handle(MenuDeletedEvent notification, CancellationToken cancellationToken)
    {
        return HandleInternal(notification.menu.Id.Value, cancellationToken);
    }

    private async Task HandleInternal(Guid id, CancellationToken cancellationToken)
    {
        await _cacheService.RemoveAsync("get-all-menus");
        await _cacheService.RemoveAsync($"menu-by-id-{id}");
    }
}