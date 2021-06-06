using LoyaltyPrime.Domain;
using LoyaltyPrime.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LoyaltyPrime.Infrastructure.Configurations
{
    public class MemberConfigurations : BaseEntityConfigurations<Members>
    {
        public override void Configure(EntityTypeBuilder<Members> builder)
        {
            builder.ToTable("Member", "dbo");

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(250);

            builder.Property(e => e.Address)
                .IsRequired().HasMaxLength(250);
        }
    }
}
