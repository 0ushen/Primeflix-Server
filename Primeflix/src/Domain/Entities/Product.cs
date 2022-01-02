namespace Primeflix.Domain.Entities;

public class Product : AuditableEntity
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Year { get; set; }
    public string Rated { get; set; }
    public string Released { get; set; }
    public string Runtime { get; set; }
    public string Genre { get; set; }
    public string Director { get; set; }
    public string Writer { get; set; }
    public string Actors { get; set; }
    public string Plot { get; set; }
    public string Language { get; set; }
    public string Country { get; set; }
    public int Stars { get; set; }
    public string ImdbID { get; set; }
    public decimal Price { get; set; }
    public decimal SalePrice { get; set; }
    public int Discount { get; set; }
    public int Stock { get; set; }

    public IList<Picture> Pictures { get; private set; } = new List<Picture>();
}
