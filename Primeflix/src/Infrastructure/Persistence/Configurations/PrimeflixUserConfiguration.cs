using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Primeflix.Domain.Entities;

namespace Primeflix.Infrastructure.Persistence.Configurations;

public class PrimeflixUserConfiguration : IEntityTypeConfiguration<PrimeflixUser>
{
    public void Configure(EntityTypeBuilder<PrimeflixUser> builder)
    {
    }
}