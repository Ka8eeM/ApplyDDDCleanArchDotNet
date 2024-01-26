using MediatR;
using Project.Application.Common.Interfaces.Presistance;
using Project.Application.Common.Interfaces.UnitOfWorks;
using Project.Application.Messaging;
using Project.Domain.Common.Errors;
using Project.Domain.MenuAggregate;
using Project.Domain.MenuAggregate.Entities;
using Project.Domain.MenuAggregate.Events;

namespace Project.Application.Menus.Commands.CreateMenu;

internal class CreateMenuCommandHandler : ICommandHandler<CreateMenuCommand, CreateMenuCommandResponse>
{
    private readonly IMenuRepository _menuRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPublisher _publisher;
    public CreateMenuCommandHandler(
        IMenuRepository menuRepository,
        IUnitOfWork unitOfWork,
        IPublisher publisher)
    {
        _menuRepository = menuRepository;
        _unitOfWork = unitOfWork;
        _publisher = publisher;
    }

    public async Task<Result<CreateMenuCommandResponse>> Handle(CreateMenuCommand request, CancellationToken cancellationToken)
    {
        // Create Menu
        var menu = Menu.Create(
            name: request.Name,
            description: request.Description,
            request.Average,
            request.Rating,
            hostId: request.HostId,
            sections: request.Sections.ConvertAll(section => MenuSection.Create(
                section.Name,
                section.Description,
                section.Items.ConvertAll(item => MenuItem.Create(
                    item.Name,
                    item.Description)))));

        // Persist Menu

        await _menuRepository.Add(menu);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var menuCreatedEvent = new MenuCreatedEvent(menu);

        await _publisher.Publish(menuCreatedEvent, cancellationToken);

        // Return MenuId
        return Result.Success(new CreateMenuCommandResponse(menu.Id.Value));
    }
}