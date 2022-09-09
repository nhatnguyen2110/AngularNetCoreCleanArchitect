using CleanArchitecture.Application.Accounts.Dtos;
using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CleanArchitecture.Infrastructure.Identity;

public class IdentityService : IIdentityService
{
    //private readonly UserManager<ApplicationUser> _userManager;
    //private readonly IUserClaimsPrincipalFactory<ApplicationUser> _userClaimsPrincipalFactory;
    private readonly IAuthorizationService _authorizationService;
    private readonly ICommonService _commonService;
    private readonly IConfiguration _configuration;
    private readonly ITokenService _tokenService;

    public IdentityService(
        //UserManager<ApplicationUser> userManager,
        //IUserClaimsPrincipalFactory<ApplicationUser> userClaimsPrincipalFactory,
        IAuthorizationService authorizationService,
        ICommonService commonService,
        IConfiguration configuration,
        ITokenService tokenService
        )
    {
        //_userManager = userManager;
        //_userClaimsPrincipalFactory = userClaimsPrincipalFactory;
        _authorizationService = authorizationService;
        _commonService = commonService;
        _configuration = configuration;
        _tokenService = tokenService;
    }

    //public async Task<string> GetUserNameAsync(string userId)
    //{
    //    var user = await _userManager.Users.FirstAsync(u => u.Id == userId);

    //    return user.UserName;
    //}

    //public async Task<(Result Result, string UserId)> CreateUserAsync(string userName, string password)
    //{
    //    var user = new ApplicationUser
    //    {
    //        UserName = userName,
    //        Email = userName,
    //    };

    //    var result = await _userManager.CreateAsync(user, password);

    //    return (result.ToApplicationResult(), user.Id);
    //}

    //public async Task<bool> IsInRoleAsync(string userId, string role)
    //{
    //    var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

    //    return user != null && await _userManager.IsInRoleAsync(user, role);
    //}

    //public async Task<bool> AuthorizeAsync(string userId, string policyName)
    //{
    //    var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

    //    if (user == null)
    //    {
    //        return false;
    //    }

    //    var principal = await _userClaimsPrincipalFactory.CreateAsync(user);

    //    var result = await _authorizationService.AuthorizeAsync(principal, policyName);

    //    return result.Succeeded;
    //}

    //public async Task<Result> DeleteUserAsync(string userId)
    //{
    //    var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

    //    return user != null ? await DeleteUserAsync(user) : Result.Success();
    //}

    //public async Task<Result> DeleteUserAsync(ApplicationUser user)
    //{
    //    var result = await _userManager.DeleteAsync(user);

    //    return result.ToApplicationResult();
    //}

    public async Task<SignInResultDto> AuthorizeAsync(string emailOrPhoneNo, string passcode, bool keepLogin, LoginMethod loginMethod, CancellationToken cancellationToken)
    {
        Account _account;
        switch (loginMethod)
        {
            case LoginMethod.Email_Password:
            case LoginMethod.Email_VerificationCode:
            case LoginMethod.Social_Login:
                //get account by email
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
                _account = await _commonService.ApplicationDBContext.Accounts.FirstOrDefaultAsync(a => a.Email == emailOrPhoneNo.Trim().ToLower());
                if (_account == null)
                {
                    throw new IdentityException("Cannot find Email");
                }
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
                break;
            default:
                throw new IdentityException("Login Method is not supported");
        }
        //check account is activated
        if (!_account.ActivatedEnable)
        {
            throw new IdentityException("Account is deactivated");
        }
        //check account is blocked
        var lockoutDurationInMinutes = int.Parse(_configuration["ApplicationSettings:LockoutDurationInMinutes"] ?? "15");
        if (_account.LockoutEnabled && _account.LockoutEnd.HasValue)
        {
            if (_account.LockoutEnd.GetValueOrDefault() > DateTime.Now)
            {

                _account.LockoutEnd = DateTime.Now.AddMinutes(lockoutDurationInMinutes);
                await this._commonService.ApplicationDBContext.SaveChangesAsync(cancellationToken);
                throw new IdentityException($"Too many failed login attempts. Please try again in {lockoutDurationInMinutes} minutes.");
            }
            else
            {
                //reset access failed count
                _account.AccessFailedCount = 0;
            }
        }

        switch (loginMethod)
        {
            case LoginMethod.Email_Password:
                //check password
                if (!BCrypt.Net.BCrypt.Verify(passcode, _account.PasswordHash))//wrong password
                {
                    var maxLoginFailedCount = int.Parse(_configuration["ApplicationSettings:MaxLoginFailedCount"] ?? "5");
                    _account.AccessFailedCount++;
                    if (_account.AccessFailedCount >= maxLoginFailedCount)
                    {
                        _account.LockoutEnabled = true;
                        _account.LockoutEnd = DateTime.Now.AddMinutes(lockoutDurationInMinutes);
                    }
                    await this._commonService.ApplicationDBContext.SaveChangesAsync(cancellationToken);
                    throw new IdentityException($"Username or password is incorrect");
                }
                break;
            case LoginMethod.Email_VerificationCode:
                //check code
                var code = _commonService.CacheService.GetByKey<string>($"EmailVerificationCode_{_account.Email}");
                if (code == null)
                {
                    throw new IdentityException($"Verification code is expired");
                }
                if (passcode != code)
                {
                    var maxLoginFailedCount = int.Parse(_configuration["ApplicationSettings:MaxLoginFailedCount"] ?? "5");
                    _account.AccessFailedCount++;
                    if (_account.AccessFailedCount >= maxLoginFailedCount)
                    {
                        _account.LockoutEnabled = true;
                        _account.LockoutEnd = DateTime.Now.AddMinutes(lockoutDurationInMinutes);
                    }
                    await this._commonService.ApplicationDBContext.SaveChangesAsync(cancellationToken);
                    throw new IdentityException($"Verification code is incorrect");
                }

                break;
            case LoginMethod.Social_Login:
                break;
            default:
                throw new IdentityException("Login Method is not supported");
        }

        //gen token
        _account.AccessFailedCount = 0;
        _account.LockoutEnabled = false;
        _account.LockoutEnd = null;
        var isFirstLogin = false;
        if (!_account.LastLoginDate.HasValue)
        {
            isFirstLogin = true;
        }
        _account.LastLoginDate = DateTime.Now;
        await this._commonService.ApplicationDBContext.SaveChangesAsync(cancellationToken);
        var jwtToken = await _tokenService.GenerateJWToken(new UserTokenModel
        {
            Id = _account.Id.ToString(),
            Email = "" + _account.Email,
            ExpireInMinutes = int.Parse(keepLogin ? _configuration["JWTSettings:KeepLoginDurationInMinutes"] : _configuration["JWTSettings:DefaultDurationInMinutes"])
        });
        var response = new SignInResultDto
        {
            AccessToken = jwtToken,
            Account = _commonService.Mapper?.Map<AccountDto>(_account)
        };
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        response.Account.IsFirstLogin = isFirstLogin;
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        return response;
    }

    public async Task<SignInResultDto> RefreshTokenAsync(string accessToken)
    {
        var claimsPrincipal = _tokenService.DecryptTokenToClaim(accessToken, false);
        if (claimsPrincipal == null)
        {
            throw new IdentityException("Invalid Token");
        }
        var _user = await _tokenService.GetUserToken(claimsPrincipal);
        var _account = await _commonService.ApplicationDBContext.Accounts.FirstOrDefaultAsync(a => a.Id == int.Parse(_user.Id));
        if (_account == null)
        {
            throw new IdentityException("Invalid User");
        }
        var jwtToken = await _tokenService.GenerateJWToken(_user);
        var response = new SignInResultDto
        {
            AccessToken = jwtToken,
            Account = _commonService.Mapper?.Map<AccountDto>(_account)
        };
        return response;
    }
}
