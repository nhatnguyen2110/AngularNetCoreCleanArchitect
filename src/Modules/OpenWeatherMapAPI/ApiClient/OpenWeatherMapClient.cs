using CleanArchitecture.Domain.ApiClient;
using CleanArchitecture.Domain.Cache;
using OpenWeatherMapAPI.Models;
using OpenWeatherMapAPI.Shared;

namespace OpenWeatherMapAPI.ApiClient;

public class OpenWeatherMapClient : ApiHttpClient, IOpenWeatherMapClient
{
    private readonly OpenWeatherMapSettings _openWeatherMapSettings;
    private readonly ICacheService _cacheService;
    public OpenWeatherMapClient(System.Net.Http.HttpClient httpClient, OpenWeatherMapSettings openWeatherMapSettings, ICacheService cacheService) : base(httpClient)
    {
        this._openWeatherMapSettings = openWeatherMapSettings;
        this._cacheService = cacheService;
        this.BaseUrl = openWeatherMapSettings.host;
        this.ReadResponseAsString = false;
    }
    public override Task PrepareRequestAsync(System.Net.Http.HttpClient client, System.Net.Http.HttpRequestMessage request, string url, object obj)
    {
        //json body
        var content_ = new System.Net.Http.StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(obj, _settings.Value));
        content_.Headers.ContentType = System.Net.Http.Headers.MediaTypeHeaderValue.Parse("application/json");
        request.Content = content_;
        request.Headers.Accept.Add(System.Net.Http.Headers.MediaTypeWithQualityHeaderValue.Parse("application/json"));

        ////application/x-www-form-urlencoded body
        //request.Headers.TryAddWithoutValidation("Content-Type", ConvertToString("application/x-www-form-urlencoded", System.Globalization.CultureInfo.InvariantCulture));
        //var nvc = this.ConvertToKeyValuePairList(obj);
        //request.Content = new FormUrlEncodedContent(nvc);

        return Task.CompletedTask;
    }

    public async Task<OWPOneCallRes> GetDailyForecastIn7DaysAsync(OWPDailyForecastIn7DaysReq request, CancellationToken cancellationToken = default)
    {
        if (request == null)
            throw new System.ArgumentNullException("request");
        var relativeUrl = $"onecall?appid={_openWeatherMapSettings.appid}&lat={request.Lat}&lon={request.Lon}";
        if (!string.IsNullOrEmpty(request.Exclude))
        {
            relativeUrl += $"&exclude={request.Exclude}";
        }
        if (!string.IsNullOrEmpty(request.Units))
        {
            relativeUrl += $"&units={request.Units}";
        }
        else if (!string.IsNullOrEmpty(_openWeatherMapSettings.units))
        {
            relativeUrl += $"&units={_openWeatherMapSettings.units}";
        }
        return await this.SendRequestAsync<OWPOneCallRes>(null, relativeUrl, new { }, CleanArchitecture.Domain.Enums.HttpMethod.GET, cancellationToken);
    }
    public async Task<OWPHistorialOneCallRes> GetHistoricalWeatherAsync(OWPHistoricalWeatherReq request, CancellationToken cancellationToken = default)
    {
        if (request == null)
            throw new System.ArgumentNullException("request");
        var relativeUrl = $"onecall/timemachine?appid={_openWeatherMapSettings.appid}&lat={request.Lat}&lon={request.Lon}&dt={request.Dt}";
        if (!string.IsNullOrEmpty(request.Units))
        {
            relativeUrl += $"&units={request.Units}";
        }
        else if (!string.IsNullOrEmpty(_openWeatherMapSettings.units))
        {
            relativeUrl += $"&units={_openWeatherMapSettings.units}";
        }
        return await this.SendRequestAsync<OWPHistorialOneCallRes>(null, relativeUrl, new { }, CleanArchitecture.Domain.Enums.HttpMethod.GET, cancellationToken);
    }
}
