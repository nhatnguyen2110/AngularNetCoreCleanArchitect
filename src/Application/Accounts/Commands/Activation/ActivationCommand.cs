using CleanArchitecture.Application.Common.Handlers;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Domain;
using CleanArchitecture.Domain.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Application.Accounts.Commands.Activation;
public class ActivationCommand : IRequest<Response<Unit>>
{
    public string? code { get; set; }
    public string requestId { get; set; } = Guid.NewGuid().ToString();
}
public class ActivationCommandHandler : BaseHandler<ActivationCommand, Response<Unit>>
{
    private readonly ApplicationSettings _applicationSettings;
    public ActivationCommandHandler(ICommonService commonService,
        ILogger<ActivationCommand> logger,
        ApplicationSettings applicationSettings
        ) : base(commonService, logger)
    {
        this._applicationSettings = applicationSettings;
    }
    public async override Task<Response<Unit>> Handle(ActivationCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var decryptedData = string.Empty;
            try
            {
#pragma warning disable CS8604 // Possible null reference argument.
                decryptedData = CommonHelper.Decrypt(System.Web.HttpUtility.UrlDecode(request.code), _applicationSettings.EncryptPassword);
#pragma warning restore CS8604 // Possible null reference argument.
            }
            catch (Exception ex)
            {
                return new Response<Unit>(false, "Invalid activation code", ex.Message, "Failed to activate Account", request.requestId);
            }
            var arrData = decryptedData.Split("<|>");
            if (arrData.Length < 2)
            {
                return new Response<Unit>(false, "Invalid activation code", "no expiry date", "Failed to activate Account");
            }
            int userId = 0;
            DateTime createdDate = DateTime.MinValue;

            int.TryParse(arrData[0], out userId);
            DateTime.TryParse(arrData[1], out createdDate);
            if (userId == 0)
            {
                return new Response<Unit>(false, "Invalid activation code", "invalid user id", "Failed to activate Account");
            }
            if (createdDate.AddMinutes(_applicationSettings.ExpireEmailConfirmInMinutes) < DateTime.Now)
            {
                return new Response<Unit>(false, "Activation code is expired", "create date: " + createdDate, "Failed to activate Account");
            }
            var entity = await _commonService.ApplicationDBContext.Accounts.FirstOrDefaultAsync(x => x.Id == userId);
            if (entity == null)
            {
                return new Response<Unit>(false, "Invalid activation code", "cannot find user", "Failed to activate Account");
            }
            entity.ActivatedEnable = true;
            await _commonService.ApplicationDBContext.SaveChangesAsync(cancellationToken);
            return Response<Unit>.Success(Unit.Value);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to activate Account. Request: {Name} {@Request}", typeof(ActivationCommand).Name, request);
            return new Response<Unit>(false, Constants.GeneralErrorMessage, ex.Message, "Failed to activate Account", request.requestId);
        }
    }
}