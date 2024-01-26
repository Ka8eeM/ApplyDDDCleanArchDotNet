using MediatR;
using Project.Application.Common.Interfaces.Presistance;
using Project.Application.Common.Interfaces.UnitOfWorks;
using Project.Application.Messaging;
using Project.Domain.Common.Errors;
using Project.Domain.MenuAggregate;
using Project.Domain.MenuAggregate.Errors;
using Project.Domain.MenuAggregate.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Menus.Commands.DeleteMenu;

internal sealed class DeleteMenuCommandHandler : ICommandHandler<DeleteMenuCommand>
{
    private readonly IMenuRepository _menuRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPublisher _publisher;

    public DeleteMenuCommandHandler(
        IMenuRepository menuRepository,
        IUnitOfWork unitOfWork,
        IPublisher publisher)
    {
        _menuRepository = menuRepository;
        _unitOfWork = unitOfWork;
        _publisher = publisher;
    }



    public async Task<Result> Handle(DeleteMenuCommand request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        Menu? menu = await _menuRepository.GetMenuById(request.id);

        if (menu is null)
            return Result.Failure(MenuErrors.MenuNotFound);

        _menuRepository.Remove(menu);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var menuDeletedEvent = new MenuDeletedEvent(menu);

        await _publisher.Publish(menuDeletedEvent, cancellationToken);

        return Result.Success();
    }
}
