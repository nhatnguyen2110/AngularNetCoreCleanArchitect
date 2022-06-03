using OpenWeatherMapAPI.Models;

namespace OpenWeatherMapAPI.ApiClient;

public interface IOpenWeatherMapClient
{
    public Task<OWPOneCallRes> GetDailyForecastIn7DaysAsync(OWPDailyForecastIn7DaysReq request, CancellationToken cancellationToken = default(CancellationToken));
    public Task<OWPHistorialOneCallRes> GetHistoricalWeatherAsync(OWPHistoricalWeatherReq request, CancellationToken cancellationToken = default(CancellationToken));

}
