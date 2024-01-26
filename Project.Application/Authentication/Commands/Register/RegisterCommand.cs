using MediatR;
using Project.Application.Authentication.Common;
using Project.Application.Messaging;
using Project.Domain.Common.Errors;

namespace Project.Application.Authentication.Commands.Register;

public sealed record RegisterCommand(
    string firstName,
    string lastName,
    string email,
    string password) : ICommand<AuthenticationResult>;
