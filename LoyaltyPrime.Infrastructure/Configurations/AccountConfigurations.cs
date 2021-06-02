using LoyaltyPrime.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LoyaltyPrime.Infrastructure.Configurations
{
    public class AccountConfigurations : BaseEntityConfigurations<Account>
    {
        public override void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.ToTable("Accounts", "dbo");

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(250);

            builder.Property(e => e.Balance)
                .IsRequired().HasDefaultValue(0);

            builder.Property<int>("MemberID")
                .IsRequired();

            builder.Property(e => e.IsActive)
                .IsRequired().HasDefaultValue(true);
        }
    }
}
