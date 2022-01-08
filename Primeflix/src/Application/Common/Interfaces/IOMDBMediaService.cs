using Primeflix.Application.OMDB.Models;

namespace Primeflix.Application.Common.Interfaces;

public interface IOMDBMediaService
{
    public Task<OMDBMediaResult?> GetMediaById(OMDBIdRequest request);

    public Task<OMDBMediaResult?> GetMediaByTitle(OMDBTitleRequest request);

    public Task<OMDBSearchResult?> SearchMedias(OMDBSearchRequest request);
}
