using OpenWeatherMapAPI.Models;

namespace OpenWeatherMapAPI.ApiClient;

public interface IOpenWeatherMapClient
{
    Task<OWPOneCallRes> GetDailyForecastIn7DaysAsync(OWPDailyForecastIn7DaysReq request, CancellationToken cancellationToken = default(CancellationToken));
    Task<OWPHistorialOneCallRes> GetHistoricalWeatherAsync(OWPHistoricalWeatherReq request, CancellationToken cancellationToken = default(CancellationToken));

}
