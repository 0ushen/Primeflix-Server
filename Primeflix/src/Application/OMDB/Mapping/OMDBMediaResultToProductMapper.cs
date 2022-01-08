using AutoMapper;
using Primeflix.Application.OMDB.Models;
using Primeflix.Domain.Entities;

namespace Primeflix.Application.OMDB.Mapping;

public class OMDBMediaResultToProductMapper : Profile
{
    private readonly Random _random = new();

    public OMDBMediaResultToProductMapper()
    {
        CreateMap<OMDBMediaResult, Product>()
            .ForMember(o => o.ImdbID, i => i.MapFrom(x => x.imdbID))
            .ForMember(o => o.Discount, i => i.MapFrom(_ => _random.Next(1, 10) * 10))
            .ForMember(o => o.SalePrice, i => i.MapFrom(_ => _random.Next(10, 25)))
            .ForMember(o => o.Price, i => i.MapFrom<PriceResolver>())
            .ForMember(o => o.Stock, i => i.MapFrom(_ => _random.Next(10)))
            .ForMember(o => o.Stars, i => i.MapFrom<RatingResolver>())
            .ForMember(o => o.Pictures, i => i.MapFrom(x => new List<Picture>{
                new()
                {
                    SmallUrl = x.Poster,
                    BigUrl = x.Poster,
                }}));
    }
}
