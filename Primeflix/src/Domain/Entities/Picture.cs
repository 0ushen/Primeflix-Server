namespace Primeflix.Domain.Entities;

public class Picture : AuditableEntity
{
    public int Id { get; set; }

    public string SmallUrl { get; set; }
    public string BigUrl { get; set; }

    public Product Product { get; set; } = null!;
}
