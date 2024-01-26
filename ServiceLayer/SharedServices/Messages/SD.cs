using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedServices.Messages
{
    public class SD
    {
        public static class Mail
        {
            public const string EmailSent = "Email Sent successfully.";
            public const string EmailNotSent = "Email delivery failed.";
            public const string EmailCheck = "Please check your email.";
        }
        public static class Decode
        {
            public const string ConfirmEmailFailed = "Email confirmed failed";
            public const string ConfirmEmailFailedUserId = "Error loading user with ID.";
            public const string ConfirmEmailSuccess = "Email confirmed successfully. ";

            public const string ResetPasswordFailed = "Password Resets failed.";
            public const string ResetPasswordSuccess = "Password Resets successfully.";

        }
        public static class ReturnUrl
        {
            // change this baseServerUrl with real url
            public const string BaseServerUrl = "https://localhost:44397/";
            public const string EmailConfirmationPath = "Account/SignUpEmailConfirmed";
            public const string ResetPasswordConfirmationPath = "Account/ResetPassword";


        }
    }
}
