using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Domain;
using CleanArchitecture.Infrastructure.Identity;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.WebUI.Controllers;

public class CacheController : ApiControllerBase
{
    private readonly ILogger<CacheController> _logger;
    private readonly ICacheService _cacheService;
    private readonly IDateTime _dateTimeService;
    public CacheController(
        ILogger<CacheController> logger,
        ICacheService cacheService,
        IDateTime dateTimeService
        )
    {
        _logger = logger;
        _cacheService = cacheService;
        _dateTimeService = dateTimeService;
    }
    [CustomAuthorize]
    [HttpDelete("[action]")]
    public ActionResult ClearAll()
    {
        try
        {
            var total = _cacheService.RemoveAllCache();
            return Ok(Response<int>.Success(total));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Failed to get clear all cache. Message: {ex.Message}");
            return BadRequest(new Response(false, Constants.GeneralErrorMessage, ex.Message, "Failed to clear all cache"));
        }
    }
    [CustomAuthorize]
    [HttpDelete("[action]")]
    public ActionResult ClearByKey([FromQuery] string key)
    {
        try
        {
            _cacheService.Remove(key);
            return Ok(Response<Unit>.Success());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Failed to get clear cache by  key: {key}. Message: {ex.Message}");
            return BadRequest(new Response(false, Constants.GeneralErrorMessage, ex.Message, "Failed to clear cache"));
        }
    }
}
