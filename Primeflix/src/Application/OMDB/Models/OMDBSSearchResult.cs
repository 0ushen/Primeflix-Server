using Primeflix.Application.OMDB.Models;

public class OMDBSearchResult
{
    public List<OMDBSearchMediaResult> Search { get; set; }
    public string totalResults { get; set; }
    public string Response { get; set; }
}