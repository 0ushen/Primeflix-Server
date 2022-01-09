using AutoMapper;
using Primeflix.Application.Common.Mappings;
using Primeflix.Application.Common.Models;
using Primeflix.Domain.Entities;

namespace Primeflix.Application.Account.Models;

public class PrimeflixUserDto : IMapFrom<PrimeflixUser>
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public AddressDto Address { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<PrimeflixUser, PrimeflixUserDto>();
        //.ForMember(d => d.Address,
        //    opt => opt.MapFrom((user, _, addressDto, context) => context.Mapper.Map(user.Address, addressDto)));
    }
}