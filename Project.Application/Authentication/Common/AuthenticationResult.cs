

using Project.Domain.UserAggregate;

namespace Project.Application.Authentication.Common;

public record AuthenticationResult(
    User User,
    string Token);
