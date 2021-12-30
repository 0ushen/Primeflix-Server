using Microsoft.EntityFrameworkCore;
using Primeflix.Domain.Entities;

namespace Primeflix.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = new());

    DbSet<Product> Products { get; }
}
