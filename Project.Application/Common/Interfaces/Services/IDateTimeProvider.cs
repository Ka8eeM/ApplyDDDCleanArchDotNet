namespace Project.Application.Interfaces.Services;

public interface IDateTimeProvider
{
    DateTime UtcNow { get; }
    DateTime LocalTime { get; }
}

