﻿using CleanArchitecture.Application.Common.Handlers;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Domain;
using CleanArchitecture.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Application.Countries.Commands.CreateCountry;

public class CreateCountryCommand : IRequest<Response<int>>
{
    public string? Name { get; set; }
    public int Priority { get; set; }
    public string? LanguageCode { get; set; }
    public string? IconUrl { get; set; }
    public string? UserDefined1 { get; set; }
    public string? UserDefined2 { get; set; }
    public string? UserDefined3 { get; set; }
    public string requestId { get; set; } = Guid.NewGuid().ToString();
}
public class CreateCountryCommandHandler : BaseHandler<CreateCountryCommand, Response<int>>
{
    public CreateCountryCommandHandler(ICommonService commonService
         , ILogger<CreateCountryCommand> logger
        ) : base(commonService)
    {
    }
    public async override Task<Response<int>> Handle(CreateCountryCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var entity = new Country()
            {
                Name = request.Name,
                LanguageCode = request.LanguageCode,
                Priority = request.Priority,
                IconUrl = request.IconUrl,
                UserDefined1 = request.UserDefined1,
                UserDefined2 = request.UserDefined2,
                UserDefined3 = request.UserDefined3
            };
            this._commonService.ApplicationDBContext.Countries.Add(entity);
            await this._commonService.ApplicationDBContext.SaveChangesAsync(cancellationToken);
            return Response<int>.Success(entity.Id, request.requestId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to Create Country. Request: {Name} {@Request}", typeof(CreateCountryCommand).Name, request);
            return new Response<int>(false, Constants.GeneralErrorMessage, ex.Message, "Failed to Create Country", request.requestId);
        }
    }
}
