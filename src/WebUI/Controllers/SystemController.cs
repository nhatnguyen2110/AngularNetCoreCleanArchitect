using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Domain.Common;
using CleanArchitecture.WebUI.Models;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.WebUI.Controllers;
public class SystemController : ApiControllerBase
{
    private readonly ApplicationSettings _applicationSettings;
    public SystemController(ApplicationSettings applicationSettings)
    {
        _applicationSettings = applicationSettings;
    }

    [HttpGet("[action]")]
    public IActionResult GetPublicKey()
    {
#pragma warning disable CS8604 // Possible null reference argument.
        return Ok(
            Response<string>.Success(_applicationSettings.PublicKeyEncode)
            );
#pragma warning restore CS8604 // Possible null reference argument.
    }
    [HttpPost("[action]")]
    public IActionResult RSAEncryptData([FromBody] EncryptedDataRequestModel request)
    {
#pragma warning disable CS8604 // Possible null reference argument.
        return Ok(
            Response<object>.Success(
                new
                {
                    EncryptedData1 = CommonHelper.RSAEncrypt(request.PlainText1, _applicationSettings.PublicKeyEncode),
                    EncryptedData2 = CommonHelper.RSAEncrypt(request.PlainText2, _applicationSettings.PublicKeyEncode)
                }
                )
            );
#pragma warning restore CS8604 // Possible null reference argument.
    }
}
