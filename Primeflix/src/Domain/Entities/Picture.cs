namespace Primeflix.Domain.Entities;

public class Picture : AuditableEntity, IHasDomainEvent
{
    public List<DomainEvent> DomainEvents { get; set; }
    public int Id { get; set; }

    public Product Product { get; set; } = null!;
}
