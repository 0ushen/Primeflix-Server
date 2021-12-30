namespace Primeflix.Application.OMDB.Models;

public class OMDBTitleRequest
{
    /// <summary>
    /// Movie title to search for.
    /// </summary>
    public string  t { get; set; }
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
    /// Return short or full plot.
    /// Can be of : short, full
    /// </summary>
    public string plot { get; set; }
    /// <summary>
    /// The data type to return.
    /// Can be of : json, xml
    /// </summary>
    public string  r { get; set; } 
    /// <summary>
    /// JSONP callback name.
    /// </summary>
    public string callback { get; set; }
    /// <summary>
    /// API version (reserved for future use).
    /// </summary>
    public string v { get; set; }
}
