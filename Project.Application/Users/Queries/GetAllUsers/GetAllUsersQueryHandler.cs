using MapsterMapper;
using MediatR;
using Project.Application.Common.Interfaces.Presistance;
using Project.Application.Users.Commons;
using Project.Domain.Common.Errors;

namespace Project.Application.Users.Queries.GetAllUsers;

internal sealed class GetAllUsersQueryHandler :
    IRequestHandler<GetAllUsersQuery, Result<List<UserDto>>>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public GetAllUsersQueryHandler(
        IUserRepository userRepository,
        IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<Result<List<UserDto>>> Handle(
        GetAllUsersQuery request,
        CancellationToken cancellationToken)
    {
        var users = await _userRepository.GetAllUsersAsync();

        if (users is null)
            return Result.Failure<List<UserDto>>(new Error("NoUsersFound", "there are no users to return."));
        var mappedUsers = _mapper.Map<List<UserDto>>(users);
        return mappedUsers;
    }
}
