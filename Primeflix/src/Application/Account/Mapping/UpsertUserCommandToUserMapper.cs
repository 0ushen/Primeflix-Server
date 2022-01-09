using AutoMapper;
using Primeflix.Application.Account.Commands;
using Primeflix.Application.Common.Models;
using Primeflix.Domain.Entities;

namespace Primeflix.Application.Account.Mapping;

public class UpsertUserCommandToUserMapper : Profile
{
    public UpsertUserCommandToUserMapper()
    {
        CreateMap<UpsertUserCommand, PrimeflixUser>()
            .ForMember(o => o.Address,
                i => i.MapFrom((command, _, _, context) => context.Mapper.Map<AddressDto, Address>(command.Address)));
    }
}