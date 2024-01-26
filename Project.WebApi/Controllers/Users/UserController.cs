using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.Application.Authentication.Queries.Login;
using Project.Application.Users.Commands.UpdateUser;
using Project.Application.Users.Queries.GetAllUsers;
using Project.Contracts.Authentication;
using Project.Contracts.Users.GetAllUsers;
using Project.Contracts.Users.UpdateUser;

namespace Project.WebApi.Controllers.Users
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ApiController
    {
        private readonly IMapper _mapper;
        public UserController(
            IMapper mapper,
            ISender sender) : base(sender)
        {
            _mapper = mapper;
        }


        [HttpPost("Update/{Id}")]
        public async Task<IActionResult> UpdateUser(
            Guid Id,
            UpdateUserRequest request)
        {
            var command = _mapper.Map<UpdateUserCommand>((request, Id));

            var commandResult = await Sender.Send(command);
            if (commandResult.IsFailure)
            {
                return HandleFailure(commandResult);
            }
            return Ok(commandResult.Value);
        }

        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers([FromQuery] GetAllUsersRequest request)
        {

            var query = _mapper.Map<GetAllUsersQuery>(request);
            var commandResult = await Sender.Send(query);
            if (commandResult.IsFailure)
            {
                return HandleFailure(commandResult);
            }
            return Ok(commandResult.Value);
        }
    }
}