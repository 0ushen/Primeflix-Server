using System.Globalization;
using AutoMapper;
using Primeflix.Application.OMDB.Models;
using Primeflix.Domain.Entities;

namespace Primeflix.Application.OMDB.Mapping;

public class RatingResolver : IValueResolver<OMDBMovieResult, Product, int>
{
    public int Resolve(OMDBMovieResult source, Product destination, int destMember, ResolutionContext context)
    {
        var succeeded = decimal.TryParse(source.imdbRating, NumberStyles.Float, CultureInfo.InvariantCulture, out var result);

        if (!succeeded)
            return 0;

        var roundedDecimalValue = Math.Round(result / 2);

        return (int)roundedDecimalValue;
    }
}


