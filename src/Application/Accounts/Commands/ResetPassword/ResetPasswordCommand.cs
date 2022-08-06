using CleanArchitecture.Application.Common.Handlers;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Domain;
using CleanArchitecture.Domain.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Application.Accounts.Commands.ResetPassword;
public class ResetPasswordCommand : IRequest<Response<Unit>>
{
    public string? Code { get; set; }
    public string? NewPassword { get; set; }
    public string requestId { get; set; } = Guid.NewGuid().ToString();
}
public class ResetPasswordCommandHandler : BaseHandler<ResetPasswordCommand, Response<Unit>>
{
    private readonly ApplicationSettings _applicationSettings;
    public ResetPasswordCommandHandler(ICommonService commonService,
        ILogger<ResetPasswordCommand> logger,
        ApplicationSettings applicationSettings
        ) : base(commonService, logger)
    {
        this._applicationSettings = applicationSettings;
    }
    public async override Task<Response<Unit>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var decryptedData = string.Empty;
            try
            {
#pragma warning disable CS8604 // Possible null reference argument.
                decryptedData = CommonHelper.Decrypt(System.Web.HttpUtility.UrlDecode(request.Code), _applicationSettings.EncryptPassword);
#pragma warning restore CS8604 // Possible null reference argument.
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to reset password. Request: {Name} {@Request}", typeof(ResetPasswordCommand).Name, request);
                return new Response<Unit>(false, "Invalid code", ex.Message, "Failed to reset passowrd", request.requestId);
            }
            var arrData = decryptedData.Split("<|>");
            if (arrData.Length < 2)
            {
                return new Response<Unit>(false, "Invalid code", "no expiry date", "Failed to reset passowrd", request.requestId);
            }
            int userId = 0;
            DateTime createdDate = DateTime.MinValue;

            int.TryParse(arrData[0], out userId);
            DateTime.TryParse(arrData[1], out createdDate);
            if (userId == 0)
            {
                return new Response<Unit>(false, "Invalid code", "invalid user id", "Failed to reset passowrd", request.requestId);
            }
            if (createdDate.AddMinutes(_applicationSettings.ExpireEmailResetPasswordInMinutes) < DateTime.Now)
            {
                return new Response<Unit>(false, "Code is expired", "create date: " + createdDate, "Failed to reset passowrd", request.requestId);
            }
            var entity = await _commonService.ApplicationDBContext.Accounts.FirstOrDefaultAsync(x => x.Id == userId);
            if (entity == null)
            {
                return new Response<Unit>(false, "Invalid code", "cannot find user", "Failed to reset passowrd", request.requestId);
            }
            entity.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);

            await _commonService.ApplicationDBContext.SaveChangesAsync(cancellationToken);
            return Response<Unit>.Success(Unit.Value);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to reset password. Request: {Name} {@Request}", typeof(ResetPasswordCommand).Name, request);
            return new Response<Unit>(false, Constants.GeneralErrorMessage, ex.Message, "Failed to reset password", request.requestId);
        }
    }
}
