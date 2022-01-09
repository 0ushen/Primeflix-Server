namespace Primeflix.Domain.Entities;

public class Address : AuditableEntity
{
    public int Id { get; set; }
    public string Country { get; set; }
    public string City { get; set; }
    public string Street { get; set; }
    public string Number { get; set; }
    public string PostalCode { get; set; }
    public string? POBox { get; set; }
    public IList<PrimeflixUser> Users { get; } = new List<PrimeflixUser>();
}