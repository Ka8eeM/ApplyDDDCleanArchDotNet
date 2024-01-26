using MediatR;
namespace Project.Application.Authentication.Commands.Register;

public record UserRegisteredEvent : INotification
{
    public Guid Id { get; init; }
    public string UserName { get; init; } = string.Empty;
}