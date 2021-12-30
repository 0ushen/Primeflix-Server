using System.Reflection;
using System.Text.Json;
using System.Web;
using Microsoft.Extensions.Configuration;
using Primeflix.Application.Common.Interfaces;
using Primeflix.Application.OMDB.Models;

namespace Primeflix.Infrastructure.Services;

public class OMDBMovieService : IOMDBMovieService
{
    private readonly HttpClient _httpClient;
    private readonly string _baseUrl; 

    public OMDBMovieService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        var omdbBaseUrl = configuration["OMDB:BaseUrl"];
        var apiKey = configuration["OMDB:ApiKey"];
        _baseUrl = $"{omdbBaseUrl}?apikey={apiKey}";
    }

    public async Task<OMDBMovieResult> GetMovieById(OMDBIdRequest request)
    {
        var result = await ExecuteQuery<OMDBMovieResult, OMDBIdRequest>(request);

        return result;
    }

    public async Task<OMDBMovieResult> GetMovieByTitle(OMDBTitleRequest request)
    {
        var result = await ExecuteQuery<OMDBMovieResult, OMDBTitleRequest>(request);

        return result;
    }

    public async Task<OMDBSearchResult> SearchMovies(OMDBSearchRequest request)
    {

        var result = await ExecuteQuery<OMDBSearchResult, OMDBSearchRequest>(request);

        return result;
    }

    private async Task<TResultModel> ExecuteQuery<TResultModel, TRequestModel>(TRequestModel requestModel)
        where TRequestModel : class, new()
    {
        var url = _baseUrl + "&" + GetQueryString(requestModel);
        var responseString = await _httpClient.GetStringAsync(url);

        var result = JsonSerializer.Deserialize<TResultModel>(responseString);

        return result;
    }

    private static string GetQueryString<TRequestModel>(TRequestModel requestModel) 
        where TRequestModel : class, new()
    {
        var properties = requestModel.GetType()
                                     .GetProperties()
                                     .Where(p => p.GetValue(requestModel, null) != null)
                                     .Select(p => p.Name + "=" + HttpUtility.UrlEncode(p.GetValue(requestModel)?.ToString()));

        return string.Join("&", properties.ToArray());
    }
}
