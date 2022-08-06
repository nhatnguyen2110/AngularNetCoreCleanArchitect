using CleanArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Persistence.Configurations;

public class AccountRoleConfiguration : IEntityTypeConfiguration<AccountRole>
{
    public void Configure(EntityTypeBuilder<AccountRole> builder)
    {
        builder.HasKey(ar => new { ar.AccountId, ar.RoleId });
        builder.HasOne<Account>(ar => ar.Account)
            .WithMany(a => a.AccountRoles)
            .HasForeignKey(ar => ar.AccountId);
        builder.HasOne<SysRole>(ar => ar.Role)
            .WithMany(a => a.AccountRoles)
            .HasForeignKey(ar => ar.RoleId);
    }
}
