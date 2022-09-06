using CleanArchitecture.Domain.ApiClient;
using SocialNetworkAPI.Models;
using SocialNetworkAPI.Shared;

namespace SocialNetworkAPI.ApiClient;
public class SocialNetworkClient : ApiHttpClient, ISocialNetworkClient
{
    private readonly SocialNetworkConfig _socialNetworkConfig;
    public SocialNetworkClient(System.Net.Http.HttpClient httpClient, SocialNetworkConfig socialNetworkConfig) : base(httpClient)
    {
        this._socialNetworkConfig = socialNetworkConfig;
        this.ReadResponseAsString = false;
    }
    public override Task PrepareRequestAsync(System.Net.Http.HttpClient client, System.Net.Http.HttpRequestMessage request, string url, object obj)
    {
        //json body
        var content_ = new System.Net.Http.StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(obj, _settings.Value));
        content_.Headers.ContentType = System.Net.Http.Headers.MediaTypeHeaderValue.Parse("application/json");
        request.Content = content_;
        request.Headers.Accept.Add(System.Net.Http.Headers.MediaTypeWithQualityHeaderValue.Parse("application/json"));

        return Task.CompletedTask;
    }
    public async Task<FBMeResponse> GetFacebookProfileAsync(FBMeReq request, CancellationToken cancellationToken = default)
    {
        if (request == null)
            throw new System.ArgumentNullException("request");
        this.BaseUrl = "" + _socialNetworkConfig.Facebook_APIUrl;
        var relativeUrl = $"{_socialNetworkConfig.Facebook_AppVer}/me?access_token={request.Access_Token}&fields=email,first_name,last_name,picture";

        return await this.SendRequestAsync<FBMeResponse>(null, relativeUrl, new { }, CleanArchitecture.Domain.Enums.HttpMethod.GET, cancellationToken);
    }
}
