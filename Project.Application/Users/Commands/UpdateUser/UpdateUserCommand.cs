using MediatR;
using Project.Domain.Common.Errors;

namespace Project.Application.Users.Commands.UpdateUser;

public sealed record UpdateUserCommand(
    Guid UserId,
    string FirstName,
    string LastName) : IRequest<Result<string>>;
