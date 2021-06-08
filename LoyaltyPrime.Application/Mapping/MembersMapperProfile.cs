using AutoMapper;
using LoyaltyPrime.Application.Members.Commands.ImportMembers;
using LoyaltyPrime.Domain.Entities;

namespace LoyaltyPrime.Application.Mapping
{
    public class MembersMapperProfile : Profile
    {
        public MembersMapperProfile()
        {

            CreateMap<ImportMembersDto.AccountsDto, Account>()
                .ForMember(x => x.Name, opts => opts.MapFrom(src => src.Name))
                .ForMember(x => x.Balance, opts => opts.MapFrom(src => src.Balance))
                 .ForMember(x => x.IsActive, opts => opts.MapFrom(src => ConverterStr(src.Status)));

            CreateMap<ImportMembersDto, Member>()
                .ForMember(x => x.Name, opts => opts.MapFrom(src => src.Name))
                .ForMember(x => x.Address, opts => opts.MapFrom(src => src.Address))
                .ForMember(x => x.Accounts, opts => opts.MapFrom(src => src.Accounts));

        }

        public bool ConverterStr(string value)
        {
            return value.ToLower() == "active" ? true : false;
        }
    }
}
