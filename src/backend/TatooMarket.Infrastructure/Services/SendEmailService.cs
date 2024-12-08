using MailKit.Net.Smtp;
using Microsoft.Extensions.Azure;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatooMarket.Domain.Repositories.Services;

namespace TatooMarket.Infrastructure.Services
{
    public class SendEmailService : ISendEmailService
    {
        private readonly string _fromEmail;
        private readonly string _fromName;
        private readonly string _password;

        public SendEmailService(string fromEmail, string fromName, string password)
        {
            _fromEmail = fromEmail;
            _fromName = fromName;
            _password = password;
        }

        public async Task SendEmail(string email, string subject, string body, string name)
        {
            var mime = new MimeMessage();
            mime.From.Add(new MailboxAddress(_fromName, _fromEmail));
            mime.To.Add(new MailboxAddress(name, email));

            mime.Subject = subject;

            var bodyBuilder = new BodyBuilder();

            bodyBuilder.HtmlBody = @$"<div style=""width: 90%; max-width: 600px; margin: 0 auto; background: #fff; border-radius: 8px; box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1); overflow: hidden; font-family: Arial, sans-serif; color: #333; line-height: 1.6;"">
                                    <div style=""background-color: #222; color: #fff; padding: 20px; text-align: center;"">
                                        <h1 style=""margin: 0; font-size: 24px;"">Tattoo Marketplace</h1>
                                    </div>
                                    <div style=""padding: 20px;"">
                                        <h2 style=""color: #222; font-size: 20px;"">Hello {name}!</h2>
                                        <p style=""margin-bottom: 20px; font-size: 16px;"">
                                            {body}
                                        </p>
                                    </div>
                                    <div style=""text-align: center; padding: 10px; font-size: 14px; background: #f4f4f4; color: #666;"">
                                        <p style=""margin: 0;"">&copy; 2024 Tattoo Marketplace. Todos os direitos reservados.</p>
                                    </div>
                                </div>
                                ";

            mime.Body = bodyBuilder.ToMessageBody();

            using(var smtp = new SmtpClient())
            {
                smtp.Connect("smtp.gmail.com", 465, true);

                smtp.Authenticate(_fromEmail, _password);

                await smtp.SendAsync(mime);

                await smtp.DisconnectAsync(true);
            }
        }
    }
}
