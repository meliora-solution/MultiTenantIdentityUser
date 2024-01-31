using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using SharedServices.Models;
using System.Text;
using static SharedServices.Messages.SD;

namespace MailServices.Services
{
    public class DecodingServices
    {
        private readonly UserManager<IdentityUser> _userManager;
        public DecodingServices(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<ServiceResponseDto<bool>> ConfirmEmail(string UserId, string code)
        {
            ServiceResponseDto<bool> response = new();
            response.Message = Decode.ConfirmEmailFailed;
            response.Success = false;

            try
            {
                var user = await _userManager.FindByIdAsync(UserId);

                if (user is null)
                {
                    response.Message = Decode.ConfirmEmailFailedUserId;
                    return response;
                }

                var token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
                var result = await _userManager.ConfirmEmailAsync(user, token);
                if (result.Succeeded) response.Success = true;

                response.Message = result.Succeeded ? Decode.ConfirmEmailSuccess : Decode.ConfirmEmailFailed;

                return response;

            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                return response;
            }

        }
        public async Task<ServiceResponseDto<bool>> ResetPassword(string Email, string code, string password)
        {
            ServiceResponseDto<bool> response = new();
            response.Message = Decode.ResetPasswordFailed;
            response.Success = false;

            var user = await _userManager.FindByEmailAsync(Email);

            if (user is null)
            {

                response.Message = Decode.ResetPasswordSuccess;
                response.Success = true;
                return response;
                // Don't reveal that the user does not exist

            }
            var token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));


            var result = await _userManager.ResetPasswordAsync(user, token, password);
            if (result.Succeeded) response.Success = true;
            response.Message = result.Succeeded ? Decode.ResetPasswordSuccess : Decode.ResetPasswordFailed;

            return response;

        }
    }
}
