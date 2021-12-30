namespace Primeflix.Application.Common.Interfaces;

public interface ISeederService
{
    public Task SeedAsync(CancellationToken cancellationToken = new());
}
