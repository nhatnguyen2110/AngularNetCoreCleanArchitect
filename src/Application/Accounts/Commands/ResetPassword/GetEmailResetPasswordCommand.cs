using CleanArchitecture.Application.Common.Handlers;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Domain;
using CleanArchitecture.Domain.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Application.Accounts.Commands.ResetPassword;
public class GetEmailResetPasswordCommand : IRequest<Response<Unit>>
{
    public string? Email { get; set; }
    public string? RecaptchaToken { get; set; }
    public string requestId { get; set; } = Guid.NewGuid().ToString();
}
public class GetEmailResetPasswordCommandHandler : BaseHandler<GetEmailResetPasswordCommand, Response<Unit>>
{
    private readonly ApplicationSettings _applicationSettings;
    public GetEmailResetPasswordCommandHandler(ICommonService commonService,
        ILogger<GetEmailResetPasswordCommand> logger,
        ApplicationSettings applicationSettings
        ) : base(commonService, logger)
    {
        this._applicationSettings = applicationSettings;
    }
    public async override Task<Response<Unit>> Handle(GetEmailResetPasswordCommand request, CancellationToken cancellationToken)
    {
        try
        {

#pragma warning disable CS8602 // Dereference of a possibly null reference.
            var entity = await _commonService.ApplicationDBContext.Accounts.AsNoTracking().FirstOrDefaultAsync(x => x.Email.ToLower() == request.Email.Trim().ToLower());
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            if (entity == null)
            {
                return new Response<Unit>(false, "Cannot find email", "", "Failed to generate reset password link", request.requestId);
            }
#pragma warning disable CS8604 // Possible null reference argument.
            var verify_code = System.Web.HttpUtility.UrlEncode(CommonHelper.Encrypt($"{entity.Id}<|>{DateTime.Now}", _applicationSettings.EncryptPassword));
            var email_content = String.Format(_applicationSettings.EmailResetPassword_Content, $"{_commonService.WebsiteSettingsService.GetBaseUrl()}/reset-password?code={verify_code}");
#pragma warning restore CS8604 // Possible null reference argument.
            var email_subject = _applicationSettings.EmailResetPassword_Subject ?? "Reset your password";
            var email_displayName = _applicationSettings.SMTPDisplayName;
#pragma warning disable CS8604 // Possible null reference argument.
            _commonService.EmailService.SendEmail(
                   email_subject,
                   email_content,
                   entity.Email,
                   displayName: email_displayName
                   );
#pragma warning restore CS8604 // Possible null reference argument.
            return Response<Unit>.Success(Unit.Value, request.requestId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to generate reset password link. Request: {Name} {@Request}", typeof(GetEmailResetPasswordCommand).Name, request);
            return new Response<Unit>(false, Constants.GeneralErrorMessage, ex.Message, "Failed to generate reset password link", request.requestId);
        }
    }
}
