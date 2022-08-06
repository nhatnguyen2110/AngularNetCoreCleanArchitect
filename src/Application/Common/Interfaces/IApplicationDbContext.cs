using CleanArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<TodoList> TodoLists { get; }

    DbSet<TodoItem> TodoItems { get; }

    DbSet<Province> Provinces { get; }

    DbSet<HistoricalWeatherData> HistoricalWeatherDatas { get; }

    DbSet<Country> Countries { get; }

    DbSet<Account> Accounts { get; }

    DbSet<SocialAccountToken> SocialAccountTokens { get; }

    DbSet<SysRole> SysRoles { get; }

    DbSet<AccountRole> AccountRoles { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
