using MediatR;
using Project.Application.Common.Interfaces.Presistance;
using Project.Application.Common.Interfaces.UnitOfWorks;
using Project.Domain.Common.Errors;
using Project.Domain.UserAggregate;

namespace Project.Application.Users.Commands.UpdateUser;

internal sealed class UpdateUserCommandHandler
    : IRequestHandler<UpdateUserCommand, Result<string>>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork unitOfWork;
    public UpdateUserCommandHandler(
        IUserRepository userRepository,
        IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        this.unitOfWork = unitOfWork;
    }

    public async Task<Result<string>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        var user = returnUser(request.UserId) as User;
        if (user is null)
            return Result.Failure<string>(DomainErrors.User.UserNotFound);


        user.SetUserFirstName(request.FirstName);
        user.SetUserLastName(request.LastName);

        var ret = _userRepository.UpdateAsync(user);
        //await unitOfWork.SaveChangesAsync(cancellationToken);
        return ret.ToString();
    }

    private User? returnUser(Guid userId) => _userRepository.GetUserById(userId);
}