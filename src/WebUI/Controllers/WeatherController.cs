using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Application.GetWeatherConditions.Queries.GetWeatherConditions;
using CleanArchitecture.Application.WeatherData.Commands.DeleteWeatherData;
using CleanArchitecture.Application.WeatherData.Commands.UpdateOrCreateWeatherData;
using CleanArchitecture.Application.WeatherData.Dtos;
using CleanArchitecture.Application.WeatherData.Queries.GetLocalDataHistoricalWeather;
using CleanArchitecture.Application.WeatherData.Queries.GetWeatherForecastIn7Days;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.WebUI.Controllers;
public class WeatherController : ApiControllerBase
{
    [HttpGet("[action]")]
    public async Task<ActionResult<Response<WeatherForecastDto>>> GetForecastWeatherIn7Days([FromQuery] GetWeatherForecastIn7daysQuery request)
    {
        var response = await Mediator.Send(request);
        if (response.Succeeded)
        {
            return response;
        }
        else
        {
            return BadRequest(response);
        }
    }

    [HttpGet("[action]")]
    public async Task<ActionResult<Response<PaginatedList<DailyForecastWeatherDto>>>> GetLastLocalHistoricalWeatherQuery([FromQuery] GetLastLocalHistoricalWeatherQuery request)
    {
        var response = await Mediator.Send(request);
        if (response.Succeeded)
        {
            return response;
        }
        else
        {
            return BadRequest(response);
        }
    }

    [Authorize(Roles = "Administrator,Member")]
    [HttpPost("[action]")]
    public async Task<ActionResult<Response<int>>> UpdateOrCreateWeatherData([FromBody] UpdateOrCreateWeatherDataCommand request)
    {
        var response = await Mediator.Send(request);
        if (response.Succeeded)
        {
            return response;
        }
        else
        {
            return BadRequest(response);
        }
    }

    [Authorize(Roles = "Administrator,Member")]
    [HttpDelete("[action]")]
    public async Task<ActionResult<Response<MediatR.Unit>>> Delete([FromBody] DeleteWeatherDataCommand request)
    {
        var response = await Mediator.Send(request);
        if (response.Succeeded)
        {
            return response;
        }
        else
        {
            return BadRequest(response);
        }
    }
    [Authorize]
    [HttpGet("[action]")]
    public async Task<ActionResult<Response<WeatherConditionCollectionDto>>> GetWeatherConditions([FromQuery] GetWeatherConditionQuery request)
    {
        var response = await Mediator.Send(request);
        if (response.Succeeded)
        {
            return response;
        }
        else
        {
            return BadRequest(response);
        }
    }

}
