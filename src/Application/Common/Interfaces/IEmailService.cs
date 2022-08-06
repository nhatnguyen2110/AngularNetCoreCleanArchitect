using System.Net.Mail;

namespace CleanArchitecture.Application.Common.Interfaces;

public interface IEmailService
{
    void SendEmail(string subject, string emailContent, string emailTo, string? cc = null, string? bcc = null, string? displayName = null, Attachment[]? attachments = null);
}
