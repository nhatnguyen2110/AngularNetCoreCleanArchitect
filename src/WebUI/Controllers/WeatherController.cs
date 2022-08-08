using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Application.GetWeatherConditions.Queries.GetWeatherConditions;
using CleanArchitecture.Application.WeatherData.Commands.CreateWeatherData;
using CleanArchitecture.Application.WeatherData.Commands.DeleteWeatherData;
using CleanArchitecture.Application.WeatherData.Commands.UpdateWeatherData;
using CleanArchitecture.Application.WeatherData.Dtos;
using CleanArchitecture.Application.WeatherData.Queries.GetLocalDataHistoricalWeather;
using CleanArchitecture.Application.WeatherData.Queries.GetWeatherForecastIn7Days;
using CleanArchitecture.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CleanArchitecture.WebUI.Controllers;
public class WeatherController : ApiControllerBase
{
    private readonly ILogger<WeatherController> _logger;
    public WeatherController(
        ILogger<WeatherController> logger
        )
    {
        _logger = logger;
    }
    [HttpGet("[action]")]
    public async Task<ActionResult<Response<WeatherForecastDto>>> GetForecastWeatherIn7Days([FromQuery] GetWeatherForecastIn7daysQuery request)
    {
        try
        {
            var response = await Mediator.Send(request);
            if (response.Succeeded)
            {
                return response;
            }
            else
            {
                _logger.LogError($"Failed to get weather data (Request: {JsonConvert.SerializeObject(request)}). Response: {JsonConvert.SerializeObject(response)}");
                return BadRequest(response);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Failed to get weather data (Request: {JsonConvert.SerializeObject(request)}). Message: {ex.Message}");
            return BadRequest(new Response(false, Constants.GeneralErrorMessage, ex.Message, "Failed to Load Forecast Weather"));
        }
    }

    [AllowAnonymous]
    [HttpGet("[action]")]
    public async Task<ActionResult<Response<PaginatedList<DailyForecastWeatherDto>>>> GetLastLocalHistoricalWeatherQuery([FromQuery] GetLastLocalHistoricalWeatherQuery request)
    {
        try
        {
            var response = await Mediator.Send(request);
            if (response.Succeeded)
            {
                return response;
            }
            else
            {
                _logger.LogError($"Failed to get weather data (Request: {JsonConvert.SerializeObject(request)}). Response: {JsonConvert.SerializeObject(response)}");
                return BadRequest(response);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Failed to get weather data (Request: {JsonConvert.SerializeObject(request)}). Message: {ex.Message}");
            return BadRequest(new Response(false, Constants.GeneralErrorMessage, ex.Message, "Failed to Load Forecast Weather"));
        }
    }

    [Authorize]
    [HttpPost("[action]")]
    public async Task<ActionResult<Response<int>>> Create([FromBody] CreateWeatherDataCommand request)
    {
        try
        {
            var response = await Mediator.Send(request);
            if (response.Succeeded)
            {
                return response;
            }
            else
            {
                _logger.LogError($"Failed to create weather data (Request: {JsonConvert.SerializeObject(request)}). Response: {JsonConvert.SerializeObject(response)}");
                return BadRequest(response);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Failed to create weather data (Request: {JsonConvert.SerializeObject(request)}). Message: {ex.Message}");
            return BadRequest(new Response(false, Constants.GeneralErrorMessage, ex.Message, "Failed to Create Weather Data"));
        }
    }
    [Authorize]
    [HttpPut("[action]")]
    public async Task<ActionResult<Response<MediatR.Unit>>> Update([FromBody] UpdateWeatherDataCommand request)
    {
        try
        {
            var response = await Mediator.Send(request);
            if (response.Succeeded)
            {
                return response;
            }
            else
            {
                _logger.LogError($"Failed to update weather data (Request: {JsonConvert.SerializeObject(request)}). Response: {JsonConvert.SerializeObject(response)}");
                return BadRequest(response);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Failed to update weather data (Request: {JsonConvert.SerializeObject(request)}). Message: {ex.Message}");
            return BadRequest(new Response(false, Constants.GeneralErrorMessage, ex.Message, "Failed to Update Weather Data"));
        }
    }
    [Authorize]
    [HttpDelete("[action]")]
    public async Task<ActionResult<Response<MediatR.Unit>>> Delete([FromBody] DeleteWeatherDataCommand request)
    {
        try
        {
            var response = await Mediator.Send(request);
            if (response.Succeeded)
            {
                return response;
            }
            else
            {
                _logger.LogError($"Failed to delete weather data (Request: {JsonConvert.SerializeObject(request)}). Response: {JsonConvert.SerializeObject(response)}");
                return BadRequest(response);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Failed to delete weather data (Request: {JsonConvert.SerializeObject(request)}). Message: {ex.Message}");
            return BadRequest(new Response(false, Constants.GeneralErrorMessage, ex.Message, "Failed to Delete Weather Data"));
        }
    }
    [Authorize]
    [HttpGet("[action]")]
    public async Task<ActionResult<Response<WeatherConditionCollectionDto>>> GetWeatherConditions([FromQuery] GetWeatherConditionQuery request)
    {
        try
        {
            var response = await Mediator.Send(request);
            if (response.Succeeded)
            {
                return response;
            }
            else
            {
                _logger.LogError($"Failed to get weather conditions (Request: {JsonConvert.SerializeObject(request)}). Response: {JsonConvert.SerializeObject(response)}");
                return BadRequest(response);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Failed to get weather conditions (Request: {JsonConvert.SerializeObject(request)}). Message: {ex.Message}");
            return BadRequest(new Response(false, Constants.GeneralErrorMessage, ex.Message, "Failed to get weather conditions"));
        }
    }

}
