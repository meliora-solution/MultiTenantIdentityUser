using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;

namespace SharedServices.Services.Email
{
    public class EncodingServices
    {
        private UserManager<IdentityUser> _userManager;
        public EncodingServices(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<string> GetEncodedConfirmationTokenUrl(IdentityUser user, string baseUri, string path)
        {


            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);


            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var tokenGenerated = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            byte[] tokenGeneratedBytes = Encoding.UTF8.GetBytes(tokenGenerated);
            var codeEncoded = WebEncoders.Base64UrlEncode(tokenGeneratedBytes);


            var queryparams = new Dictionary<string, string>()
            {
                {"userid", user.Id },
                { "code", codeEncoded}
            };
            var callbackUrl = QueryHelpers.AddQueryString(baseUri + path, queryparams);
            return callbackUrl;


        }
        public async Task<string> GetEncodedResetPasswordTokenUrl(IdentityUser user, string baseuri, string path)
        {
            var code = await _userManager.GeneratePasswordResetTokenAsync(user);


            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var tokenGenerated = await _userManager.GeneratePasswordResetTokenAsync(user);

            byte[] tokenGeneratedBytes = Encoding.UTF8.GetBytes(tokenGenerated);
            var codeEncoded = WebEncoders.Base64UrlEncode(tokenGeneratedBytes);


            var queryparams = new Dictionary<string, string>()
            {
                {"userid", user.Id },
                { "code", codeEncoded}
            };

            var callbackUrl = QueryHelpers.AddQueryString(baseuri + path, queryparams);
            return callbackUrl;

        }
    }
}

