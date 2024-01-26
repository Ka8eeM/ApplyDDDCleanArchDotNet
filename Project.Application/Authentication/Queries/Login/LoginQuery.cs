using MediatR;
using Project.Application.Authentication.Common;
using Project.Application.Messaging;
using Project.Domain.Common.Errors;

namespace Project.Application.Authentication.Queries.Login;

public sealed record LoginQuery(
    string email,
    string password) : IQuery<AuthenticationResult>;