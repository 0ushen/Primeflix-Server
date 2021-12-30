using Primeflix.Application.OMDB.Models;

namespace Primeflix.Application.Common.Interfaces;

public interface IOMDBMovieService
{
    public Task<OMDBMovieResult> GetMovieById(OMDBIdRequest request);

    public Task<OMDBMovieResult> GetMovieByTitle(OMDBTitleRequest request);

    public Task<OMDBSearchResult> SearchMovies(OMDBSearchRequest request);
}
