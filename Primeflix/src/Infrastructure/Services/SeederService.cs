﻿using AutoMapper;
using Primeflix.Application.Common.Interfaces;
using Primeflix.Application.OMDB.Models;
using Primeflix.Domain.Entities;

namespace Primeflix.Infrastructure.Services;

public class SeederService : ISeederService
{
    private readonly IList<string> _moviesAbout = new List<string>
    {
        "Batman",
        "Avengers",
        "Spiderman"
    };

    private readonly IOMDBMovieService _omdbMovieService;
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public SeederService(
        IOMDBMovieService omdbMovieService, 
        IApplicationDbContext dbContext,
        IMapper mapper)
    {
        _omdbMovieService = omdbMovieService;
        _dbContext = dbContext;
        _mapper = mapper;
    }
    public async Task SeedAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        if(!_dbContext.Products.Any())
            await SeedMoviesToDatabase();
    }

    private async Task SeedMoviesToDatabase()
    {
        foreach (var movieType in _moviesAbout)
            await AddMoviesAbout(movieType);
    }

    private async Task AddMoviesAbout(string movieType)
    {
        var movies = await GetMoviesAbout(movieType);

        var products = _mapper.Map<List<OMDBMovieResult>, List<Product>>(movies);

        await _dbContext.Products.AddRangeAsync(products);
        await _dbContext.SaveChangesAsync();

    }

    private async Task<List<OMDBMovieResult>> GetMoviesAbout(string movieType)
    {
        var movies = new List<OMDBMovieResult>();

        var moviePreviews = await _omdbMovieService.SearchMovies(new OMDBSearchRequest
        {
            s = movieType,
        });

        foreach (var imdbId in moviePreviews.Search.Select(x => x.imdbID))
        {
            var fullMovie = await _omdbMovieService.GetMovieById(new OMDBIdRequest
            {
                i = imdbId,
                plot = "full"
            });

            fullMovie.Summary = (await _omdbMovieService.GetMovieById(new OMDBIdRequest
            {
                i = imdbId,
                plot = "small"
            })).Plot;

            movies.Add(fullMovie);
        }

        return movies;
    }
}


