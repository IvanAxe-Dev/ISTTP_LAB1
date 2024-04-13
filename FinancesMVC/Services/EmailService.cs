using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace FinancesMVC.Services
{
    public class EmailService : IEmailSender
    {
        private readonly AuthMessageSenderOptions _options;

        public EmailService(IOptions<AuthMessageSenderOptions> optionsAccessor)
        {
            _options = optionsAccessor.Value;
        }

        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            return Execute(_options.SendGridKey, subject, htmlMessage, email);
        }

        public Task Execute(string apiKey, string subject, string body, string email)
        {
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress("finances@gmail.com", "Your financial manager"),
                Subject = subject,
                PlainTextContent = body,
                HtmlContent = body
            };
            msg.AddTo(new EmailAddress(email));

            // Disable click tracking.
            // See https://sendgrid.com/docs/User_Guide/Settings/tracking.html
            msg.SetClickTracking(false, false);

            return client.SendEmailAsync(msg);
        }
    }

    public class AuthMessageSenderOptions
    {
        public string SendGridKey { get; set; }
    }
}
