using AutoMapper;
using Primeflix.Domain.Entities;

namespace Primeflix.Application.Common.Models;

public class AddressDto
{
    public string Country { get; set; }
    public string City { get; set; }
    public string PostalCode { get; set; }
    public string Street { get; set; }
    public string Number { get; set; }
    public string? POBox { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Address, AddressDto>();
    }
}