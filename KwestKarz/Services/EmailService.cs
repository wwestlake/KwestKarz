using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using KwestKarz.Services.Configuration;
using KwestKarz.Entities;

namespace KwestKarz.Services
{

    public class EmailService : IEmailService
    {
        private readonly GoogleEmailSettings _settings;
        private readonly ILogService _logService;

        public EmailService(IOptions<GoogleEmailSettings> options, ILogService logService)
        {
            _settings = options.Value;
            _logService = logService;
        }

        public async Task SendEmailAsync(string to, string subject, string body)
        {
            if (string.IsNullOrWhiteSpace(body))
            {
                await _logService.LogAsync(TechLogLevel.Warning, "Attempted to send email with empty body.", detail: $"To={to}, Subject={subject}");
                throw new ArgumentException("Email body is null or empty", nameof(body));
            }

            try
            {
                var message = new MimeMessage();
                message.From.Add(MailboxAddress.Parse(_settings.Email));
                message.To.Add(MailboxAddress.Parse(to));
                message.Subject = subject;
                message.Body = new TextPart("plain") { Text = body };

                using var client = new SmtpClient();
                await client.ConnectAsync("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                await client.AuthenticateAsync(_settings.Email, _settings.Password);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);

                await _logService.LogAsync(TechLogLevel.Information, "Email sent successfully.", detail: $"To={to}, Subject={subject}");
            }
            catch (Exception ex)
            {
                await _logService.LogAsync(TechLogLevel.Error, "Failed to send email.", ex.ToString());
                throw;
            }
        }
    }
}
