using CleanArchitecture.Application.Accounts.Dtos;
using CleanArchitecture.Application.Common.Handlers;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Application.Accounts.Commands.GetEmailVerificationCode;
public class GetEmailVerificationCodeCommand : IRequest<Response<EmailVerificationCodeResultDto>>
{
    public string? Email { get; set; }
    public string requestId { get; set; } = Guid.NewGuid().ToString();
}
public class GetEmailVerificationCodeCommandHandler : BaseHandler<GetEmailVerificationCodeCommand, Response<EmailVerificationCodeResultDto>>
{
    private readonly ApplicationSettings _applicationSettings;
    public GetEmailVerificationCodeCommandHandler(ICommonService commonService,
        ILogger<GetEmailVerificationCodeCommand> logger,
        ApplicationSettings applicationSettings
        ) : base(commonService, logger)
    {
        this._applicationSettings = applicationSettings;
    }
    public async override Task<Response<EmailVerificationCodeResultDto>> Handle(GetEmailVerificationCodeCommand request, CancellationToken cancellationToken)
    {
        try
        {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            var entity = await _commonService.ApplicationDBContext.Accounts.AsNoTracking().FirstOrDefaultAsync(x => x.Email.ToLower() == request.Email.Trim().ToLower());
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            if (entity == null)
            {
                return new Response<EmailVerificationCodeResultDto>(false, "Cannot find email", "", "Failed to generate verification code", request.requestId);
            }
            //gen code
            var code = (new Random().Next(100000, 999999)).ToString();
            //send email
#pragma warning disable CS8604 // Possible null reference argument.
            _commonService.EmailService.SendEmail(subject: _applicationSettings.EmailVerificationCode_Subject,
                emailContent: String.Format(_applicationSettings.EmailVerificationCode_Content, code),
                emailTo: entity.Email,
                displayName: _applicationSettings.SMTPDisplayName
                );
#pragma warning restore CS8604 // Possible null reference argument.
            //store to cache
            _commonService.CacheService.Set(
                key: $"EmailVerificationCode_{entity.Email}",
                cacheTime: (this._applicationSettings.ExpireEmailVerificationCodeInSeconds / 60),
                value: code);
            return Response<EmailVerificationCodeResultDto>.Success(new EmailVerificationCodeResultDto()
            {
                ExpireInSeconds = this._applicationSettings.ExpireEmailVerificationCodeInSeconds
            }, request.requestId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to generate verification code. Request: {Name} {@Request}", typeof(GetEmailVerificationCodeCommand).Name, request);
            return new Response<EmailVerificationCodeResultDto>(false, Constants.GeneralErrorMessage, ex.Message, "Failed to generate verification code", request.requestId);
        }
    }
}
