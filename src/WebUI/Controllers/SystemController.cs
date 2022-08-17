using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Application.Configs.Dtos;
using CleanArchitecture.Application.Configs.Queries.GetSystemConfigs;
using CleanArchitecture.Domain.Common;
using CleanArchitecture.WebUI.Extensions;
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
    [HttpGet("[action]")]
    public async Task<ActionResult<Response<ConfigsDto>>> GetConfigs()
    {
        var result = await Mediator.Send(new GetSystemConfigsQuery());
        if (result.Succeeded)
        {
            if (result.Data != null)
            {
                result.Data.Version = Constants.ApiVersionName.V1;
            }
            return result;
        }
        else
        {
            return BadRequest(result);
        }
    }
}
