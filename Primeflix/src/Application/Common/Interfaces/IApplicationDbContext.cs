using Microsoft.EntityFrameworkCore;
using Primeflix.Domain.Entities;

namespace Primeflix.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Product> Products { get; }
    DbSet<Picture> Pictures { get; }
    DbSet<Address> Addresses { get; }
    DbSet<PrimeflixUser> Users { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = new());
}