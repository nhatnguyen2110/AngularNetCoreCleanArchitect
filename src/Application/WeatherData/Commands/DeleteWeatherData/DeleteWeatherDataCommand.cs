using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Handlers;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Domain;
using CleanArchitecture.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Application.WeatherData.Commands.DeleteWeatherData;

public class DeleteWeatherDataCommand : IRequest<Response<Unit>>
{
    public int Id { get; set; }
    public string requestId { get; set; } = Guid.NewGuid().ToString();
}
public class DeleteWeatherDataCommandHandler : BaseHandler<DeleteWeatherDataCommand, Response<Unit>>
{
    public DeleteWeatherDataCommandHandler(ICommonService commonService
        , ILogger<DeleteWeatherDataCommand> logger
        ) : base(commonService, logger)
    {
    }
    public async override Task<Response<Unit>> Handle(DeleteWeatherDataCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var entity = await this._commonService.ApplicationDBContext.HistoricalWeatherDatas
            .FindAsync(new object[] { request.Id }, cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(HistoricalWeatherData), request.Id);
            }
            this._commonService.ApplicationDBContext.HistoricalWeatherDatas.Remove(entity);
            await this._commonService.ApplicationDBContext.SaveChangesAsync(cancellationToken);
            return Response<Unit>.Success(Unit.Value, request.requestId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to Delete Weather Data. Request: {Name} {@Request}", typeof(DeleteWeatherDataCommand).Name, request);
            return new Response<Unit>(false, Constants.GeneralErrorMessage, ex.Message, "Failed to Delete Weather Data", request.requestId);
        }
    }

}