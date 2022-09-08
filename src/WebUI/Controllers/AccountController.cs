using CleanArchitecture.Application.Accounts.Commands.Activation;
using CleanArchitecture.Application.Accounts.Commands.FacebookLogin;
using CleanArchitecture.Application.Accounts.Commands.GetEmailVerificationCode;
using CleanArchitecture.Application.Accounts.Commands.GoogleLogin;
using CleanArchitecture.Application.Accounts.Commands.RefreshToken;
using CleanArchitecture.Application.Accounts.Commands.ResetPassword;
using CleanArchitecture.Application.Accounts.Commands.SignIn;
using CleanArchitecture.Application.Accounts.Commands.SignUp;
using CleanArchitecture.Application.Accounts.Dtos;
using CleanArchitecture.Application.Accounts.Queries;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Infrastructure.Identity;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.WebUI.Controllers;
public class AccountController : ApiControllerBase
{
    [HttpPost("[action]")]
    public async Task<ActionResult<Response<Unit>>> SignUp([FromBody] SignUpCommand command)
    {
        var result = await Mediator.Send(command);
        if (result.Succeeded)
        {
            return result;
        }
        else
        {
            return BadRequest(result);
        }
    }
    [HttpPost("[action]")]
    public async Task<ActionResult<Response<SignInResultDto>>> SignIn([FromBody] SignInCommand command)
    {
        var result = await Mediator.Send(command);
        if (result.Succeeded)
        {
            return result;
        }
        else
        {
            return BadRequest(result);
        }
    }
    [HttpPost("[action]")]
    public async Task<ActionResult<Response<Unit>>> Activation([FromBody] ActivationCommand command)
    {
        var result = await Mediator.Send(command);
        if (result.Succeeded)
        {
            return result;
        }
        else
        {
            return BadRequest(result);
        }
    }
    [HttpPost("[action]")]
    public async Task<ActionResult<Response<EmailVerificationCodeResultDto>>> GetEmailVerificationCode([FromBody] GetEmailVerificationCodeCommand command)
    {
        var result = await Mediator.Send(command);
        if (result.Succeeded)
        {
            return result;
        }
        else
        {
            return BadRequest(result);
        }
    }
    [HttpPost("[action]")]
    public async Task<ActionResult<Response<SignInResultDto>>> SignInByEmailVerificationCode([FromBody] SignInByEmailVerificationCodeCommand command)
    {
        var result = await Mediator.Send(command);
        if (result.Succeeded)
        {
            return result;
        }
        else
        {
            return BadRequest(result);
        }
    }
    [HttpPost("[action]")]
    public async Task<ActionResult<Response<Unit>>> GetEmailResetPassword([FromBody] GetEmailResetPasswordCommand command)
    {
        var result = await Mediator.Send(command);
        if (result.Succeeded)
        {
            return result;
        }
        else
        {
            return BadRequest(result);
        }
    }
    [HttpPost("[action]")]
    public async Task<ActionResult<Response<Unit>>> ResetPassword([FromBody] ResetPasswordCommand command)
    {
        var result = await Mediator.Send(command);
        if (result.Succeeded)
        {
            return result;
        }
        else
        {
            return BadRequest(result);
        }
    }
    [CustomAuthorize]
    [HttpGet("[action]")]
    public async Task<ActionResult<Response<AccountDto>>> GetProfile([FromQuery] GetProfileQuery query)
    {
        var result = await Mediator.Send(query);
        if (result.Succeeded)
        {
            return result;
        }
        else
        {
            return BadRequest(result);
        }
    }
    [HttpPost("[action]")]
    public async Task<ActionResult<Response<SignInResultDto>>> RefreshToken([FromBody] RefreshTokenCommand command)
    {
        var result = await Mediator.Send(command);
        if (result.Succeeded)
        {
            return result;
        }
        else
        {
            return BadRequest(result);
        }
    }
    [HttpPost("[action]")]
    public async Task<ActionResult<Response<SignInResultDto>>> FacebookLogin([FromBody] FacebookLoginCommand command)
    {
        var result = await Mediator.Send(command);
        if (result.Succeeded)
        {
            return result;
        }
        else
        {
            return BadRequest(result);
        }
    }
    [HttpPost("[action]")]
    public async Task<ActionResult<Response<SignInResultDto>>> GoogleLogin([FromBody] GoogleLoginCommand command)
    {
        var result = await Mediator.Send(command);
        if (result.Succeeded)
        {
            return result;
        }
        else
        {
            return BadRequest(result);
        }
    }
}
