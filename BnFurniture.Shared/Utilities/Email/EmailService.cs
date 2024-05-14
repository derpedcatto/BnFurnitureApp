using System.Net.Mail;
using System.Net;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using BnFurniture.Domain.Responses;

namespace BnFurniture.Shared.Utilities.Email;

public class EmailService : IEmailService
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<EmailService> _logger;

    public EmailService(IConfiguration configuration, ILogger<EmailService> logger)
    {
        _logger = logger;
        _configuration = configuration;
    }

    public async Task<StatusResponse> SendNewPasswordEmailAsync(
        string userEmail,
        string newPassword,
        string? userFullName,
        CancellationToken cancellationToken)
    {
        var smtpConfig = _configuration.GetSection("smtp");
        if ( ! smtpConfig.Exists())
        {
            _logger.LogError($"[SERVICE] Email NewPassword configuration loading error");
            return new StatusResponse(
                false, 
                (int)HttpStatusCode.InternalServerError,
                "Email configuration loading error");
        }

        string host = smtpConfig["host"]!;
        int port = int.Parse(smtpConfig["port"]!);
        string mailbox = smtpConfig["email"]!;
        string password = smtpConfig["password"]!;
        bool ssl = bool.Parse(smtpConfig["ssl"]!);

        string emailSubject = "Ваш новый пароль";
        string emailBody;
        if (string.IsNullOrEmpty(userFullName))
            emailBody = $"Здравствуйте, {userEmail}!";
        else
            emailBody = $"Здравствуйте, {userFullName}!";
        emailBody += $"\n\nВаш новый пароль: {newPassword}";

        var mailAddressTo = new MailAddress(userEmail);
        var mailAddressFrom = new MailAddress(mailbox);
        var mail = new MailMessage
        {
            From = mailAddressFrom,
            Subject = emailSubject,
            Body = emailBody,
            IsBodyHtml = false,
            DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure,
            Priority = MailPriority.High
        };
        mail.To.Add(mailAddressTo);

        using var smtpClient = new SmtpClient(host)
        {
            Port = port,
            DeliveryMethod = SmtpDeliveryMethod.Network,
            Credentials = new NetworkCredential(mailbox, password),
            EnableSsl = ssl
        };

        try
        {
            await smtpClient.SendMailAsync(mail, cancellationToken);
            _logger.LogInformation("[SERVICE] Email NewPassword sent success");
            return new StatusResponse(true, (int)HttpStatusCode.OK, "Email sent successfully");
        }
        catch (SmtpException ex)
        {
            _logger.LogError(ex, $"[SERVICE] Email NewPassword SMTP error: {ex.Message}");
            return new StatusResponse(false, (int)HttpStatusCode.InternalServerError, "Email sending error - SMTP error");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"[SERVICE] Email NewPassword sending error: {ex.Message}");
            return new StatusResponse(false, (int)HttpStatusCode.InternalServerError, "Email sending error");
        }
    }
}
