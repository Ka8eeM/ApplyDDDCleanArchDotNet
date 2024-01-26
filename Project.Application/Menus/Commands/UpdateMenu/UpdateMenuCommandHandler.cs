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

namespace Project.Application.Menus.Commands.UpdateMenu;

internal class UpdateMenuCommandHandler : ICommandHandler<UpdateMenuCommand, bool>
{
    private readonly IMenuRepository _menuRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPublisher _publisher;

    public UpdateMenuCommandHandler(
        IPublisher publisher,
        IUnitOfWork unitOfWork,
        IMenuRepository menuRepository)
    {
        _publisher = publisher;
        _unitOfWork = unitOfWork;
        _menuRepository = menuRepository;
    }

    public async Task<Result<bool>> Handle(UpdateMenuCommand request, CancellationToken cancellationToken)
    {
        Menu? menu = await _menuRepository.GetMenuById(request.Id);


        if (menu is null)
            return Result.Failure<bool>(MenuErrors.MenuNotFound);

        menu.SetDescription(request.Description);
        menu.SetName(request.Name);

        _menuRepository.Update(menu);

        var result = await _unitOfWork.SaveChangesAsync(cancellationToken);


        if (result.IsFailure)
        {
            return Result.Failure<bool>(result.Error);
        }

        var menuUpdatedEvent = new MenuUpdatedEvent(menu);

        await _publisher.Publish(menuUpdatedEvent, cancellationToken);

        return result.Value > 0;
    }
}
