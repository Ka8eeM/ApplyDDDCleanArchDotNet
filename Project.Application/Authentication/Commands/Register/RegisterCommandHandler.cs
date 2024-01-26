using Project.Application.Authentication.Common;
using Project.Application.Common.Interfaces.Authentication;
using Project.Application.Common.Interfaces.Presistance;
using Project.Application.Common.Interfaces.UnitOfWorks;
using Project.Application.Interfaces.Services;
using Project.Application.Messaging;
using Project.Domain.Common.Errors;
using Project.Domain.UserAggregate;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Project.Application.Authentication.Commands.Register;

internal sealed class RegisterCommandHandler :
    ICommandHandler<RegisterCommand, AuthenticationResult>
{

    private readonly IUserRepository _userRepository;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IUnitOfWork unitOfWork;
    public RegisterCommandHandler(
        IUserRepository userRepository,
        IJwtTokenGenerator jwtTokenGenerator,
        IUnitOfWork unitOfWork,
        IDateTimeProvider dateTimeProvider)
    {
        _userRepository = userRepository;
        _jwtTokenGenerator = jwtTokenGenerator;
        this.unitOfWork = unitOfWork;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<Result<AuthenticationResult>> Handle(RegisterCommand command, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        var user = returnUser(command.email);
        if (user is not null)
        {
            return Result.Failure<AuthenticationResult>(DomainErrors.User.DuplicatEmail);
        }
        user = User.CreateUser(
        Guid.NewGuid(),
            command.firstName,
            command.lastName,
            command.email,
            command.password,
            _dateTimeProvider.UtcNow);

        var userId = _userRepository.AddAsync(user);


        var token = _jwtTokenGenerator.GenerateToken(user);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new AuthenticationResult(
            User.CreateUser(
                userId,
                user?.FirstName!,
                user?.LastName!,
                user?.Email!,
                user?.Password!,
                user!.CreatedAt),
            token);
    }

    private User? returnUser(string email) => _userRepository.GetUserByEmailAsync(email);
}