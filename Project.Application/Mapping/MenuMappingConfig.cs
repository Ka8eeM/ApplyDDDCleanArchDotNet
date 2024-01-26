using Mapster;
using Project.Application.Menus.Commands.CreateMenu;
using Project.Application.Menus.Commands.DeleteMenu;
using Project.Application.Menus.Commands.UpdateMenu;
using Project.Application.Menus.Queries.GetAllMenus;
using Project.Contracts.Menus;
using Project.Domain.MenuAggregate;
using Project.Domain.MenuAggregate.Entities;

namespace Project.Application.Mapping;

public class MenuMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Menu, MenuResponse>()
            .Map(dest => dest.Id, src => src.Id.Value)
            .Map(dest => dest.AverageRating, src => src.AverageRating.Value)
            .Map(dest => dest.HostId, src => src.HostId.Value)
            .Map(dest => dest.DinnerIds, src => src.DinnerIds.Select(d => d.Value))
            .Map(dest => dest.MenuReviewIds, src => src.MenuReviewsIds.Select(d => d.Value));

        config.NewConfig<MenuSection, MenuSectionResponse>()
            .Map(dest => dest.Id, src => src.Id.Value)
            .Map(dest => dest.Items, src => src.Items.Select(i => i.Id.Value));

        config.NewConfig<MenuItem, MenuItemResponse>()
            .Map(dest => dest.Id, src => src.Id.Value);

        config.NewConfig<(CreateMenuRequest Request, string HostId), CreateMenuCommand>()
            .Map(dest => dest.HostId, src => src.HostId)
            .Map(dest => dest.Average, src => src.Request.Average)
            .Map(dest => dest.Rating, src => src.Request.Rating)
            .Map(dest => dest, src => src.Request);

        config.NewConfig<DeleteMenuRequest, DeleteMenuCommand>()
            .Map(dest => dest.id, src => src.id);

        config.NewConfig<(UpdateMenuRequest request, Guid id), UpdateMenuCommand>()
            .Map(dest => dest.Id, src => src.id)
            .Map(dest => dest.Name, src => src.request.name)
            .Map(dest => dest.Description, src => src.request.description);

        config.NewConfig<GetAllMenusRequest, GetAllMenusQuery>()
           .Map(dest => dest.Name, src => src.Name)
           .Map(dest => dest.HostId, src => src.HostId)
           .Map(dest => dest.CreatedDateTime, src => src.CreatedDateTime);
    }
}