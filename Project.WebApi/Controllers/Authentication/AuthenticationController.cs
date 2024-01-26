using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Application.Authentication.Commands.Register;
using Project.Application.Authentication.Common;
using Project.Application.Authentication.Queries.Login;
using Project.Contracts.Authentication;
using Project.Domain.Common.Errors;

namespace Project.WebApi.Controllers.Authentication
{
    [Route("auth")]
    [AllowAnonymous]
    public class AuthenticationController : ApiController
    {
        private readonly IMapper _mapper;

        public AuthenticationController(
            ISender sender,
            IMapper mapper) : base(sender)
        {
            _mapper = mapper;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest registerRequest)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            var command = _mapper.Map<RegisterCommand>(registerRequest);

            Result<AuthenticationResult> authResult = await Sender.Send(command);
            if (authResult.IsFailure)
            {
                return HandleFailure(authResult);
            }
            return CreatedAtAction(nameof(Register), authResult.Value);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);
            var query = _mapper.Map<LoginQuery>(loginRequest);
            var authResult = await Sender.Send(query);

            if (authResult.IsFailure)
            {
                return HandleFailure(authResult);
            }


            return Ok(authResult.Value);
        }
    }
}