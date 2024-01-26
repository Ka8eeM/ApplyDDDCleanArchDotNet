using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.Application.Menus.Commands.CreateMenu;
using Project.Application.Menus.Commands.DeleteMenu;
using Project.Application.Menus.Commands.UpdateMenu;
using Project.Application.Menus.Queries.GetAllMenus;
using Project.Application.Menus.Queries.GetMenuById;
using Project.Contracts.Menus;

namespace Project.WebApi.Controllers;

[Route("hosts/{hostId}/menus")]
public class MenusController : ApiController
{
    private readonly IMapper _mapper;

    public MenusController(IMapper mapper, ISender sender) : base(sender)
    {
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> CreateMenu(
        CreateMenuRequest menuRequest,
        string hostId)
    {
        var command = _mapper.Map<CreateMenuCommand>((menuRequest, hostId));
        var createMenuResult = await Sender.Send(command);
        //CreatedAtAction(nameof(GetMunu), new {hostId, menuId = menu.Id}, menu)
        if (createMenuResult.IsFailure)
        {
            return HandleFailure(createMenuResult);
        }
        return CreatedAtAction(nameof(CreateMenu), createMenuResult.Value);
    }


    [HttpDelete]
    public async Task<IActionResult> DeleteMenu(
        DeleteMenuRequest deleteMenuRequest)
    {
        var command = _mapper.Map<DeleteMenuCommand>(deleteMenuRequest);
        var deleteMenuResult = await Sender.Send(command);
        //CreatedAtAction(nameof(GetMunu), new {hostId, menuId = menu.Id}, menu)
        if (deleteMenuResult.IsFailure)
        {
            return HandleFailure(deleteMenuResult);
        }
        return NoContent();
    }

    [HttpPut("/{id}")]
    public async Task<IActionResult> UpdateMenu(
        [FromRoute] Guid id,
        [FromBody] UpdateMenuRequest updateMenuRequest)
    {
        var command = _mapper.Map<UpdateMenuCommand>((updateMenuRequest, id));
        var updateMenuResult = await Sender.Send(command);
        //CreatedAtAction(nameof(GetMunu), new {hostId, menuId = menu.Id}, menu)
        if (updateMenuResult.IsFailure)
        {
            return HandleFailure(updateMenuResult);
        }
        return NoContent();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetMenu(
        [FromRoute] Guid id)
    {
        var query = new GetMenuByIdQuery(id);
        var queryResult = await Sender.Send(query);
        //CreatedAtAction(nameof(GetMunu), new {hostId, menuId = menu.Id}, menu)
        if (queryResult.IsFailure)
        {
            return HandleFailure(queryResult);
        }
        return Ok(queryResult);
    }
    [HttpGet]
    public async Task<IActionResult> GetAllMenus(
        [FromBody] GetAllMenusRequest request)
    {
        var query = _mapper.Map<GetAllMenusQuery>(request);

        var queryResult = await Sender.Send(query);
        //CreatedAtAction(nameof(GetMunu), new {hostId, menuId = menu.Id}, menu)
        if (queryResult.IsFailure)
        {
            return HandleFailure(queryResult);
        }
        return Ok(queryResult);
    }
}