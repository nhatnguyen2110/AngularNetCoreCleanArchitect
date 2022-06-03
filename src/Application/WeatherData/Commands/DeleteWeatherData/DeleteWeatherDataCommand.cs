using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Handlers;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Domain;
using CleanArchitecture.Domain.Entities;
using MediatR;

namespace CleanArchitecture.Application.WeatherData.Commands.DeleteWeatherData;

public class DeleteWeatherDataCommand : IRequest<Response<Unit>>
{
    public int Id { get; set; }
}
public class DeleteWeatherDataCommandHandler : BaseHandler<DeleteWeatherDataCommand, Response<Unit>>
{
    public DeleteWeatherDataCommandHandler(ICommonService commonService
        ) : base(commonService)
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
            return Response<Unit>.Success(Unit.Value);
        }
        catch (Exception ex)
        {
            return new Response<Unit>(false, Constants.GeneralErrorMessage, ex.Message, "Failed to Delete Weather Data");
        }
    }

}