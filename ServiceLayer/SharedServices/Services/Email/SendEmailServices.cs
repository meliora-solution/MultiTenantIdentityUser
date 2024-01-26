using IdentityUser100.Entity;
using Microsoft.AspNetCore.Identity;
using MimeKit;
using SharedServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SharedServices.Messages.SD;

namespace SharedServices.Services.Email
{
    public class SendEmailServices
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly EmailServices _email;
        private readonly EncodingServices _encodingServices;

        public SendEmailServices(UserManager<IdentityUser> userManager,
                                 EmailServices _email,
                                 EncodingServices encodingServices)

        {
            _userManager = userManager;
            this._email = _email;
            _encodingServices = encodingServices;
        }

        public async Task<ServiceResponseDto<bool>> EmailConfirmation(string siteName, string Email, string BaseServerUrl)
        {
            ServiceResponseDto<bool> response = new();
            response.Message = Mail.EmailNotSent;
            response.Success = false;

            try
            {

                var user = await _userManager.FindByEmailAsync(Email!);

                if (user == null)
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    response.Message = Mail.EmailCheck;
                    response.Success = true;
                    return response;
                }

                string baseUri = BaseServerUrl ?? ReturnUrl.BaseServerUrl;

                string confirmationPath = ReturnUrl.EmailConfirmationPath;
                string EmailConfirmationUrl = await _encodingServices.GetEncodedConfirmationTokenUrl(user, baseUri, confirmationPath);

                var message = new MimeMessage();
                message = EmailMessageServices.RegisterMessage(siteName, user.Email, EmailConfirmationUrl);
                await _email.SendEmailAsync(user.Email, message);


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
        public async Task<ServiceResponseDto<bool>> EmailInvitation(string siteName, string Email, string EmailInvitationUrl)
        {
            ServiceResponseDto<bool> response = new();
            response.Message = Mail.EmailNotSent;
            response.Success = false;

            try
            {

                var user = await _userManager.FindByEmailAsync(Email!);

                // User should be null
                if (user!= null)
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    response.Message = Mail.EmailCheck;
                    response.Success = true;
                    return response;
                }

                //string baseUri = BaseServerUrl ?? ReturnUrl.BaseServerUrl;

                //string confirmationPath = ReturnUrl.EmailConfirmationPath;
                //string EmailConfirmationUrl = await _encodingServices.GetEncodedConfirmationTokenUrl(user, baseUri, confirmationPath);

                var message = new MimeMessage();
                message = EmailMessageServices.InvitationMessage(siteName, Email, EmailInvitationUrl);
                await _email.SendEmailAsync(Email, message);


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
        public async Task<ServiceResponseDto<bool>> ForgotPassword(string siteName,string Email, string BaseServerUrl)
        {
            ServiceResponseDto<bool> response = new();
            response.Message = Mail.EmailNotSent;
            response.Success = false;
            try
            {
                // Get User based on Email

                var user = await _userManager.FindByEmailAsync(Email);
                if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    response.Message = Mail.EmailCheck;
                    response.Success = true;
                    return response;
                }


                string baseUri = BaseServerUrl ?? ReturnUrl.BaseServerUrl;
                string ResetPasswordPath = ReturnUrl.ResetPasswordConfirmationPath;
                string ResetPasswordUrl = await _encodingServices.GetEncodedResetPasswordTokenUrl(user, baseUri, ResetPasswordPath);


                var message = new MimeMessage();
                message = EmailMessageServices.ResetPasswordMessage(siteName, user.Email, ResetPasswordUrl);
                await _email.SendEmailAsync(user.Email, message);


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
