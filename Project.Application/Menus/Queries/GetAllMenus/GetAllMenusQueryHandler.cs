using Project.Application.Common.Interfaces.Presistance;
using Project.Application.Messaging;
using Project.Domain.Common.Errors;
using Project.Domain.MenuAggregate.Errors;

namespace Project.Application.Menus.Queries.GetAllMenus;

internal sealed class GetAllMenusQueryHandler : IQueryHandler<GetAllMenusQuery, List<GetAllMenusQueryResponse>>
{
    private readonly IMenuRepository _menuRepository;

    public GetAllMenusQueryHandler(
        IMenuRepository menuRepository)
    {
        _menuRepository = menuRepository;
    }

    public async Task<Result<List<GetAllMenusQueryResponse>>> Handle(GetAllMenusQuery request, CancellationToken cancellationToken)
    {
        var result = await _menuRepository.GetAll();

        if (result is null)
        {
            return Result.Failure<List<GetAllMenusQueryResponse>>(MenuErrors.MenusAreEmpty);
        }


        var finalResult = result.Select(m =>
        {
            return new GetAllMenusQueryResponse(
                m.Id.Value,
                m.Name,
                m.Description,
                m.AverageRating.Value,
                m.AverageRating.NumRatings,
                m.HostId.Value);
        }).ToList();

        return finalResult;
    }
}
