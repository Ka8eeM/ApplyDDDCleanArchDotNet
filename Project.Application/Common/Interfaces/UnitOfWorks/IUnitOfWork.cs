using Project.Domain.Common.Errors;

namespace Project.Application.Common.Interfaces.UnitOfWorks;

public interface IUnitOfWork
{
    Task<Result<long?>> SaveChangesAsync(CancellationToken cancellationToken);
}