using Project.Domain.Common.Premitives;

namespace Project.Domain.MenuAggregate.Events;

public record MenuCreatedEvent(Menu menu) : IDomainEvent;