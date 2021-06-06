using AutoMapper;
using LoyaltyPrime.Application.Transactions.Events;
using LoyaltyPrime.Domain.Entities;

namespace LoyaltyPrime.Application.Mapping
{
    public class TransactionsMapperProfile : Profile
    {
        public TransactionsMapperProfile()
            : base("Transaction")
        {
            CreateMap<TransactionCreatedEvent, Transaction>()
                .ForMember(x => x.AccountID, opts => opts.MapFrom(src => src.AccountID))
                .ForMember(x => x.Description, opts => opts.MapFrom(src => src.Description))
                .ForMember(x => x.Amount, opts => opts.MapFrom(src => src.Point));
        }
    }
}
