using Project.Domain.UserAggregate;

namespace Project.Application.Common.Interfaces.Authentication;

public interface IJwtTokenGenerator
{

    string GenerateToken(User user);
}