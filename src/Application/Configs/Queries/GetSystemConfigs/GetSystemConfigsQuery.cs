using CleanArchitecture.Application.Common.Handlers;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Application.Configs.Dtos;
using CleanArchitecture.Domain;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Application.Configs.Queries.GetSystemConfigs;
public class GetSystemConfigsQuery : IRequest<Response<ConfigsDto>>
{
    public string requestId { get; set; } = Guid.NewGuid().ToString();
}
public class GetSystemConfigsQueryHandler : BaseHandler<GetSystemConfigsQuery, Response<ConfigsDto>>
{
    private readonly ApplicationSettings _applicationSettings;
    public GetSystemConfigsQueryHandler(ICommonService commonService
        , ILogger<GetSystemConfigsQuery> logger,
        ApplicationSettings applicationSettings
       ) : base(commonService, logger)
    {
        _applicationSettings = applicationSettings;
    }
    public async override Task<Response<ConfigsDto>> Handle(GetSystemConfigsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var result = new ConfigsDto
            {
                EnableEncryptAuthorize = _applicationSettings.EnableEncryptAuthorize,
                PublicKeyEncode = _applicationSettings.PublicKeyEncode
            };
            await Task.CompletedTask;
            return Response<ConfigsDto>.Success(result, request.requestId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to load Configs. Request: {Name} {@Request}", typeof(GetSystemConfigsQuery).Name, request);
            return new Response<ConfigsDto>(false, Constants.GeneralErrorMessage, ex.Message, "Failed to Load Configs", request.requestId);
        }
    }
}