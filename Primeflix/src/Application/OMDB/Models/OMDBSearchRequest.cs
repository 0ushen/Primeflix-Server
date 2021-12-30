namespace Primeflix.Application.OMDB.Models;

public class OMDBSearchRequest
{
    /// <summary>
    /// Movie title to search for.
    /// </summary>
    public string s { get; set; }
    /// <summary>
    /// Type of result to return.
    /// Can be of : movie, series, episode
    /// </summary>
    public string type { get; set; }
    /// <summary>
    /// Year of release.
    /// </summary>
    public int? y { get; set; }
    /// <summary>
    /// The data type to return.
    /// Can be of : json, xml
    /// </summary>
    public string  r { get; set; }
    /// <summary>
    /// Page number to return.
    /// Can be of : 1-100
    /// </summary>
    public int? page { get; set; }
    /// <summary>
    /// JSONP callback name.
    /// </summary>
    public string callback { get; set; }
    /// <summary>
    /// API version (reserved for future use).
    /// </summary>
    public string v { get; set; }
}
