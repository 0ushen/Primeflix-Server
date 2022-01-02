using AutoMapper;
using Primeflix.Application.Common.Mappings;
using Primeflix.Domain.Entities;

namespace Primeflix.Application.Products.Models;

public class ProductDto : IMapFrom<Product>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public decimal SalePrice { get; set; }
    public int Discount { get; set; }
    public List<MoviePictureDto> Pictures { get; set; }
    public string ShortDetails { get; set; }
    public string Description { get; set; }
    public int Stock { get; set; }
    public string Brand { get; set; }
    public bool NewPro { get; set; }
    public bool Sale { get; set; }
    public string Category { get; set; }
    public int Stars { get; set; }
    public string[] Tags { get; set; }
    public string[] Colors { get; set; }

    public void Mapping(Profile profile)
    {
        var gen = new Random();
        var tags = new[] {"comedy", "documentary"};
        var colors = new[] {"white", "green", "yellow"};

        profile.CreateMap<Product, ProductDto>()
            .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Title))
            .ForMember(d => d.Pictures, opt => opt.MapFrom(s => s.Pictures.Select(x => new MoviePictureDto
            {
                Small = x.SmallUrl,
                Big = x.BigUrl
            })))
            .ForMember(d => d.ShortDetails, opt => opt.MapFrom(s => s.Summary))
            .ForMember(d => d.Description, opt => opt.MapFrom(s => s.Plot))
            .ForMember(d => d.Brand, opt => opt.MapFrom(_ => "movie"))
            .ForMember(d => d.NewPro, opt => opt.MapFrom(_ => true))
            .ForMember(d => d.Sale, opt => opt.MapFrom(_ => gen.Next(100) <= 30))
            .ForMember(d => d.Category, opt => opt.MapFrom(_ => "movie"))
            .ForMember(d => d.Tags, opt => opt.MapFrom(_ => tags))
            .ForMember(d => d.Colors, opt => opt.MapFrom(_ => colors));

    }
}
