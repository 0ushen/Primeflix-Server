using AutoMapper;
using Primeflix.Application.OMDB.Models;
using Primeflix.Domain.Entities;

namespace Primeflix.Application.OMDB.Mapping;

public class PriceResolver : IValueResolver<OMDBMediaResult, Product, decimal>
{
    public decimal Resolve(OMDBMediaResult source, Product destination, decimal destMember, ResolutionContext context)
    {
        var discount = (destination.SalePrice / 100) * destination.Discount;

        return destination.SalePrice - discount;
    }
}