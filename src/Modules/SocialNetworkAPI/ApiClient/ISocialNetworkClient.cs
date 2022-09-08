using SocialNetworkAPI.Models;

namespace SocialNetworkAPI.ApiClient;
public interface ISocialNetworkClient
{
    Task<FBMeResponse> GetFacebookProfileAsync(FBMeReq request, CancellationToken cancellationToken = default(CancellationToken));
    Task<GoogleTokenInfoResponse> GetGoogleTokenInfoAsync(GoogleTokenInfoReq request, CancellationToken cancellationToken = default(CancellationToken));
}
