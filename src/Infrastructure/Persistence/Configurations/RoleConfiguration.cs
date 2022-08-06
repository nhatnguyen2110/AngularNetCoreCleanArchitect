using CleanArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Persistence.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<SysRole>
{
    public void Configure(EntityTypeBuilder<SysRole> builder)
    {
        builder.ToTable("Roles");
    }
}
