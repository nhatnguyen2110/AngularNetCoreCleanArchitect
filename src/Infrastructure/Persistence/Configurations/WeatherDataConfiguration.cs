using CleanArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Persistence.Configurations;

public class WeatherDataConfiguration : IEntityTypeConfiguration<HistoricalWeatherData>
{
    public void Configure(EntityTypeBuilder<HistoricalWeatherData> builder)
    {
        
    }
}
