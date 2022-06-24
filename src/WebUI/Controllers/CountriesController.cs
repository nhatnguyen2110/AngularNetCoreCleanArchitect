using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Application.Countries.Commands.CreateCountry;
using CleanArchitecture.Application.Countries.Commands.DeleteCountry;
using CleanArchitecture.Application.Countries.Commands.UpdateCountry;
using CleanArchitecture.Application.Countries.Queries.GetCountriesWithPagination;
using CleanArchitecture.Application.Countries.Queries.GetCountryById;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.WebUI.Controllers;
[Authorize]
public class CountriesController : ApiControllerBase
{
    [AllowAnonymous]
    [HttpGet("{id}")]
    public async Task<ActionResult<Response<CountryDto>>> Get(int id)
    {
        var result = await Mediator.Send(new GetCountryByIdQuery() { CountryId = id });
        if (result.Succeeded)
        {
            return result;
        }
        else
        {
            return BadRequest(result);
        }
    }
    [AllowAnonymous]
    [HttpGet("[action]")]
    public async Task<ActionResult<Response<PaginatedList<CountryDto>>>> GetList([FromQuery] GetCountriesWithPaginationQuery request)
    {
        var result = await Mediator.Send(request);
        if (result.Succeeded)
        {
            return result;
        }
        else
        {
            return BadRequest(result);
        }
    }
    [HttpPost("[action]")]
    public async Task<ActionResult<Response<int>>> Create([FromBody] CreateCountryCommand command)
    {
        var result = await Mediator.Send(command);
        if (result.Succeeded)
        {
            return result;
        }
        else
        {
            return BadRequest(result);
        }
    }
    [HttpPut("[action]")]
    public async Task<ActionResult<Response<MediatR.Unit>>> Update([FromBody] UpdateCountryCommand command)
    {
        var result = await Mediator.Send(command);
        if (result.Succeeded)
        {
            return result;
        }
        else
        {
            return BadRequest(result);
        }
    }
    [HttpDelete("[action]/{id}")]
    public async Task<ActionResult<Response<MediatR.Unit>>> Delete(int id)
    {
        var result = await Mediator.Send(new DeleteCountryCommand { Id = id });
        if (result.Succeeded)
        {
            return result;
        }
        else
        {
            return BadRequest(result);
        }
    }
}
