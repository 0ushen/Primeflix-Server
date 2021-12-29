using Primeflix.Application.Common.Mappings;

namespace Primeflix.Application.Products.Queries.GetProductsWithPagination;

public class ProductDto 
    //: IMapFrom<ProductItem>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public decimal SalePrice { get; set; }
    public int Discount { get; set; }
    public List<Picture> Pictures { get; set; }
    public string ShortDetails { get; set; }
    public string Description { get; set; }
    public int Stock { get; set; }
    public string Brand { get; set; }
    public bool NewPro { get; set; }
    public bool Sale { get; set; }
    public string Category { get; set; }
    public string[] Tags { get; set; }
    public string[] Colors { get; set; }
}
