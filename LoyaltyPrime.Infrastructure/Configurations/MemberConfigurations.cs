﻿using LoyaltyPrime.Domain;
using LoyaltyPrime.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LoyaltyPrime.Infrastructure.Configurations
{
    public class MemberConfigurations : BaseEntityConfigurations<Member>
    {
        public override void Configure(EntityTypeBuilder<Member> builder)
        {
            builder.ToTable("Accounts", "dbo");

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(250);

            builder.Property(e => e.Address)
                .IsRequired().HasDefaultValue(0);
        }
    }
}