﻿using AutoMapper;
using LoyaltyPrime.Application.Mapping;

namespace LoyaltyPrime.Application.Configurations
{
    public class AutoMapperProfileConfigurations
    {
        public static IMapper Initialize()
        {
            return new MapperConfiguration(config =>
            {
                config.AddProfile(new TransactionsMapperProfile());
                config.AllowNullCollections = true;
            })
            .CreateMapper();
        }
    }
}