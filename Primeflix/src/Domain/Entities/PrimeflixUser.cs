namespace Primeflix.Domain.Entities;

public class PrimeflixUser : AuditableEntity
{
    public int Id { get; set; }
    public string NameIdentifier { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public Address Address { get; set; } = null!;
}