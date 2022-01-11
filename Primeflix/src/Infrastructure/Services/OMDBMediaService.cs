using System.Text.Json;
using System.Web;
using Microsoft.Extensions.Configuration;
using Primeflix.Application.Common.Interfaces;
using Primeflix.Application.OMDB.Models;

namespace Primeflix.Infrastructure.Services;

public class OMDBMediaService : IOMDBMediaService
{
    private readonly string _baseUrl;
    private readonly HttpClient _httpClient;

    public OMDBMediaService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        var omdbBaseUrl = configuration["OMDB:BaseUrl"];
        var apiKey = configuration["OMDB:ApiKey"];
        _baseUrl = $"{omdbBaseUrl}?apikey={apiKey}";
    }

    public async Task<OMDBMediaResult?> GetMediaById(OMDBIdRequest request)
    {
        return await ExecuteQuery<OMDBMediaResult, OMDBIdRequest>(request);
    }

    public async Task<OMDBMediaResult?> GetMediaByTitle(OMDBTitleRequest request)
    {
        return await ExecuteQuery<OMDBMediaResult, OMDBTitleRequest>(request);
    }

    public async Task<OMDBSearchResult?> SearchMedias(OMDBSearchRequest request)
    {
        return await ExecuteQuery<OMDBSearchResult, OMDBSearchRequest>(request);
    }

    private async Task<TResultModel?> ExecuteQuery<TResultModel, TRequestModel>(TRequestModel requestModel)
        where TRequestModel : class, new()
    {
        var url = _baseUrl + "&" + GetQueryString(requestModel);
        var responseString = await _httpClient.GetStringAsync(url);

        return JsonSerializer.Deserialize<TResultModel>(responseString);
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