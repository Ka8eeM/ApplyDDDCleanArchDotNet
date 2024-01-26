using Project.Application.Common.Interfaces.Presistance;
using Project.Application.Menus.Queries.GetMenuById;
using Project.Application.Messaging;
using Project.Domain.Common.Errors;
using Project.Domain.MenuAggregate;
using Project.Domain.MenuAggregate.Errors;

namespace Project.Application.Menus.Queries;

internal sealed class GetMenuByIdQueryHandler : IQueryHandler<GetMenuByIdQuery, GetMenuByIdQueryResponse>
{
    private readonly IMenuRepository _menuRepository;

    public GetMenuByIdQueryHandler(IMenuRepository menuRepository)
    {
        _menuRepository = menuRepository;
    }


    public async Task<Result<GetMenuByIdQueryResponse>> Handle(
        GetMenuByIdQuery request,
        CancellationToken cancellationToken)
    {
        Menu? menu = await _menuRepository.GetMenuById(request.menuId);

        if (menu is null)
            return Result.Failure<GetMenuByIdQueryResponse>(MenuErrors.MenuNotFound);

        var result = new GetMenuByIdQueryResponse(
            menu.Id.Value,
            menu.Name,
            menu.Description,
            menu.AverageRating.Value,
            menu.AverageRating.NumRatings,
            menu.HostId.Value
            );

        return result;
    }
}