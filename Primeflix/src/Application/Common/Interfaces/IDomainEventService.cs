using Primeflix.Domain.Common;

namespace Primeflix.Application.Common.Interfaces;

public interface IDomainEventService
{
    Task Publish(DomainEvent domainEvent);
}
