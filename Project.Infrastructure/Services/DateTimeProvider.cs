using Project.Application.Interfaces.Services;

namespace Project.Infrastructure.Services;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;

    public DateTime LocalTime => DateTime.UtcNow.AddHours(2);
}