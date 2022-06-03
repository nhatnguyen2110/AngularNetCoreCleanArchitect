﻿using CleanArchitecture.Application.Common.Models;
using Microsoft.AspNetCore.Authorization;
using CleanArchitecture.Application.Provinces.Commands.CreateProvince;
using CleanArchitecture.Application.Provinces.Queries.GetProvincesWithPagination;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.WebUI.Controllers;

[Authorize]
public class ProvincesController : ApiControllerBase
{
    [HttpGet("[action]")]
    public async Task<ActionResult<Response<PaginatedList<ProvinceDto>>>> Get([FromQuery]GetProvincesWithPaginationQuery request)
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
    public async Task<ActionResult<Response<int>>> Create([FromBody] CreateProvinceCommand command)
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
    public async Task<ActionResult<Response<MediatR.Unit>>> Update([FromBody] UpdateProvinceCommand command)
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
        var result = await Mediator.Send(new DeleteProvinceCommand { Id = id });
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
