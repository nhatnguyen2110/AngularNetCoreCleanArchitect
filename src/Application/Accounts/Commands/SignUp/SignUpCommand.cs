using CleanArchitecture.Application.Common.Handlers;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Domain;
using CleanArchitecture.Domain.Common;
using CleanArchitecture.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Application.Accounts.Commands.SignUp;
public class SignUpCommand : IRequest<Response<Unit>>
{
    public string? Email { get; set; }
    public string? Password { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? AvatarUrl { get; set; }
    public string? Gender { get; set; }
    public DateTime? BirthDay { get; set; }
    public string? Address { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? ZipCode { get; set; }
    public string? Country { get; set; }
    public string? Phone { get; set; }
    public string requestId { get; set; } = Guid.NewGuid().ToString();
}
public class SignUpCommandHandler : BaseHandler<SignUpCommand, Response<Unit>>
{
    private readonly ApplicationSettings _applicationSettings;
    public SignUpCommandHandler(ICommonService commonService,
        ILogger<SignUpCommand> logger,
        ApplicationSettings applicationSettings
        ) : base(commonService, logger)
    {
        this._applicationSettings = applicationSettings;
    }
    public async override Task<Response<Unit>> Handle(SignUpCommand request, CancellationToken cancellationToken)
    {
        try
        {
            //check if email existed
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            var ent = await this._commonService.ApplicationDBContext.Accounts.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Email.ToLower() == ("" + request.Email).Trim().ToLower());
#pragma warning restore CS8602 // Dereference of a possibly null reference.

            if (ent != null)
            {
                return new Response<Unit>(false, "Email is existed", request.requestId);
            }
            var entity = new Account()
            {
                Email = ("" + request.Email).Trim().ToLower(),
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                FirstName = request.FirstName,
                LastName = request.LastName,
                AvatarUrl = request.AvatarUrl,
                Gender = request.Gender,
                BirthDay = request.BirthDay,
                Address = request.Address,
                City = request.City,
                State = request.State,
                ZipCode = request.ZipCode,
                Country = request.Country,
                Phone = request.Phone,
                ActivatedEnable = true
            };
            if (_applicationSettings.EnableEmailConfirmForRegister)
            {
                entity.ActivatedEnable = false;
            }
            this._commonService.ApplicationDBContext.Accounts.Add(entity);
            await this._commonService.ApplicationDBContext.SaveChangesAsync(cancellationToken);
            if (_applicationSettings.EnableEmailConfirmForRegister)
            {
#pragma warning disable CS8604 // Possible null reference argument.
                var verify_code = System.Web.HttpUtility.UrlEncode(CommonHelper.Encrypt($"{entity.Id}<|>{DateTime.Now}", _applicationSettings.EncryptPassword));
                var email_content = String.Format(_applicationSettings.EmailConfirm_Content, $"{_commonService.WebsiteSettingsService.GetBaseUrl()}/activation?code={verify_code}");
                var email_subject = _applicationSettings.EmailConfirm_Subject ?? "Confirmed Your account";
#pragma warning restore CS8604 // Possible null reference argument.
                _commonService.EmailService.SendEmail(
                    email_subject,
                    email_content,
                    entity.Email);
            }
            return Response<Unit>.Success(Unit.Value, request.requestId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "ailed to Sign Up. Request: {Name} {@Request}", typeof(SignUpCommand).Name, request);
            return new Response<Unit>(false, Constants.GeneralErrorMessage, ex.Message, "Failed to Sign Up", request.requestId);
        }
    }
}