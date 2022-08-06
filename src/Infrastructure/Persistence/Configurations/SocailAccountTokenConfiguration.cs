using CleanArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Persistence.Configurations;

public class SocailAccountTokenConfiguration : IEntityTypeConfiguration<SocialAccountToken>
{
    public void Configure(EntityTypeBuilder<SocialAccountToken> builder)
    {
        builder.Property(t => t.LoginProvider)
          .IsRequired();
    }
}
