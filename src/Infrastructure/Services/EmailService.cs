using System.Net.Mail;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Common;
using Microsoft.Extensions.Configuration;

namespace CleanArchitecture.Infrastructure.Services;
public class EmailService : IEmailService
{
    private readonly IConfiguration _configuration;
    public EmailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public void SendEmail(string subject, string emailContent, string emailTo, string? cc = null, string? bcc = null, string? displayName = null, Attachment[]? attachments = null)
    {
        var host = _configuration["ApplicationSettings:SMTPHost"];
        var port = _configuration["ApplicationSettings:SMTPPort"];
        var ssl = _configuration["ApplicationSettings:SMTPSSL"];
        var userName = _configuration["ApplicationSettings:SMTPUserName"];
        var password = _configuration["ApplicationSettings:SMTPPassword"];
        CommonHelper.SendMail(host, port, ssl, userName, password, userName, displayName, subject, emailContent, emailTo, cc, bcc, attachments);

    }
}
