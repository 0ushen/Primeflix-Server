using AutoMapper;
using Primeflix.Application.Common.Interfaces;
using Primeflix.Application.OMDB.Models;
using Primeflix.Domain.Entities;

namespace Primeflix.Infrastructure.Services;

public class SeederService : ISeederService
{
    private readonly IApplicationDbContext _dbContext;

    private readonly IOMDBMediaService _iomdbMediaService;
    private readonly IMapper _mapper;

    private readonly IList<string> _moviesAbout = new List<string>
    {
        "Batman",
        "Avengers",
        "Spiderman"
    };

    private readonly IList<string> _seriesAbout = new List<string>
    {
        "Friends",
        "Futurama",
        "Batman",
        "Superman"
    };

    public SeederService(
        IOMDBMediaService iomdbMediaService,
        IApplicationDbContext dbContext,
        IMapper mapper)
    {
        _iomdbMediaService = iomdbMediaService;
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task SeedAsync(CancellationToken cancellationToken = new())
    {
        if (!_dbContext.Products.Any())
            await SeedMediasToDatabase();
    }

    private async Task SeedMediasToDatabase()
    {
        foreach (var keyword in _moviesAbout)
            await AddMediasAbout(keyword, "movie");

        foreach (var keyword in _seriesAbout)
            await AddMediasAbout(keyword, "series");
    }

    private async Task AddMediasAbout(string mediaKeyword, string mediaType)
    {
        var medias = await GetMediasAbout(mediaKeyword, mediaType);

        var products = _mapper.Map<List<OMDBMediaResult>, List<Product>>(medias);

        await _dbContext.Products.AddRangeAsync(products);
        await _dbContext.SaveChangesAsync();
    }

    private async Task<List<OMDBMediaResult>> GetMediasAbout(string mediaKeyword, string mediaType)
    {
        var medias = new List<OMDBMediaResult>();

        var mediaPreviews = await _iomdbMediaService.SearchMedias(new OMDBSearchRequest
        {
            s = mediaKeyword,
            type = mediaType
        });

        if (mediaPreviews is null)
            throw new Exception("Could not fetch movie previews");

        foreach (var mediaPreview in mediaPreviews.Search.Where(moviePreview =>
                     !string.IsNullOrEmpty(moviePreview.Poster) && !string.Equals(moviePreview.Poster, "N/A")))
        {
            var fullMedia = await _iomdbMediaService.GetMediaById(new OMDBIdRequest
            {
                i = mediaPreview.imdbID,
                plot = "full"
            });

            if (fullMedia is null)
                continue;

            var mediaWithFullPlot = await _iomdbMediaService.GetMediaById(new OMDBIdRequest
            {
                i = mediaPreview.imdbID,
                plot = "small"
            });

            if (mediaWithFullPlot is null)
                continue;

            fullMedia.Summary = mediaWithFullPlot.Plot;

            medias.Add(fullMedia);
        }

        return medias;
    }
}