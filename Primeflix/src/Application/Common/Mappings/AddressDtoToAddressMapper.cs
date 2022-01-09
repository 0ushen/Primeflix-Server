using AutoMapper;
using Primeflix.Application.Common.Models;
using Primeflix.Domain.Entities;

namespace Primeflix.Application.Common.Mappings;

public class AddressDtoToAddressMapper : Profile
{
    public AddressDtoToAddressMapper()
    {
        CreateMap<AddressDto, Address>().ReverseMap();
    }
}