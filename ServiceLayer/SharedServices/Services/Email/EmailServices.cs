using Microsoft.Extensions.Options;
using MimeKit;
using SharedServices.Models;
using SharedServices.Models.Email;
using MailKit.Net.Smtp;
using static SharedServices.Messages.SD;

namespace SharedServices.Services.Email
{
    public class EmailServices
    {
        private readonly IOptions<SmtpSetting> _smtpSetting;


        public EmailServices(IOptions<SmtpSetting> smtpSetting)
        {
            this._smtpSetting = smtpSetting;
        }
        public async Task<ServiceResponseDto<bool>> SendEmailAsync(string toEmail, MimeMessage message)
        {
            ServiceResponseDto<bool> response = new();
            response.Message = Mail.EmailNotSent;
            response.Success = false;
            try
            {


                using (var emailClient = new SmtpClient())
                {
                    emailClient.Connect(_smtpSetting.Value.Host, _smtpSetting.Value.Port, MailKit.Security.SecureSocketOptions.SslOnConnect);
                    emailClient.Authenticate(_smtpSetting.Value.User, _smtpSetting.Value.Password);
                    emailClient.Send(message);

                    await emailClient.SendAsync(message);

                    emailClient.Disconnect(true);


                }

                response.Message = Mail.EmailSent;
                response.Success = true;
                return response;
            }


            catch (Exception ex)
            {
                string msg = ex.Message;
                return response;
            }

        }
    }
}
