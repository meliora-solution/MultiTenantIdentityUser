﻿using MimeKit;

namespace MailServices.Services
{
    public class EmailMessageServices
    {
        public static MimeMessage RegisterMessage(string companyName, string toEmail, string EmailConfirmationUrl, string subject = "Email Confirmation")
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Sender", "sender@yourMailServer.com"));
            message.To.Add(new MailboxAddress("Recipient", toEmail));
            message.Subject = subject;

            var builder = new BodyBuilder();

            // Set the plain-text version of the message text
            builder.TextBody = @$"Hello {toEmail},
                     You have registered for {companyName} web site.
                     To confirm that you have made this registration, please click on the email confirmation link.
                     {EmailConfirmationUrl}
                     Ignore this email if you did not register. This email is generated by a machine, you do not need to reply.";

            // Set the html version of the message text
            builder.HtmlBody = string.Format($"<p>Hello, {toEmail},<br /><br />" +
                $"<p>You have registered for {companyName} web site.<br />" +
                "To confirm that you have made this registration, please click on the email confirmation link.</p>" +
                $"<p><a href = {EmailConfirmationUrl} >Email Confirmation</a><br /><br />" +
                "<p><i>Ignore this email if you did not register. This email is generated by a machine, you do not need to reply.</i></p>");

            // Now we just need to set the message body and we're done
            message.Body = builder.ToMessageBody();

            return message;

        }

        public static MimeMessage InvitationMessage(string companyName, string toEmail, string EmailInvitationUrl, string subject = "Invitation Email")
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Sender", "sender@yourMailServer.com"));
            message.To.Add(new MailboxAddress("Recipient", toEmail));
            message.Subject = subject;

            var builder = new BodyBuilder();

            // Set the plain-text version of the message text
            builder.TextBody = @$"Hello {toEmail},
                     You are invited to join {companyName} web site.
                     To accept this invitation , please click on the email Invitation link.
                     {EmailInvitationUrl}
                     Ignore this email if you do not want to join. This email is generated by a machine, you do not need to reply.";

            // Set the html version of the message text
            builder.HtmlBody = string.Format($"<p>Hello, {toEmail},<br /><br />" +
                $"<p>You are invited to join {companyName} web site.<br />" +
                "To accept this invitation , please click on the email Invitation link.</p>" +
                $"<p><a href = {EmailInvitationUrl} >Invitation Email </a><br /><br />" +
                "<p><i> Ignore this email if you do not want to join. This email is generated by a machine, you do not need to reply.</i></p>");

            // Now we just need to set the message body and we're done
            message.Body = builder.ToMessageBody();

            return message;
        }


        public static MimeMessage ResetPasswordMessage(string companyName, string toEmail, string ResetPasswordUrl, string subject = "Reset Password")
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Sender", "sender@yourMailServer.com"));
            message.To.Add(new MailboxAddress("Recipient", toEmail));
            message.Subject = subject;

            var builder = new BodyBuilder();

            // Set the plain-text version of the message text
            builder.TextBody = @$"Halo {toEmail},
                     You have reset password for {companyName} web site.
                     To confirm that you have made this reset, please click on the Reset Password link. 
                     {ResetPasswordUrl}
                     Ignore this email if you did not do it. This email is generated by a machine, you do not need to reply.";

            // Set the html version of the message text
            builder.HtmlBody = string.Format($"<p>Halo, {toEmail},<br /><br />" +
                $"<p>You have reset password for {companyName} web site.<br />" +
                " To confirm that you have made this reset, please click on the Reset Password link.</p>" +
                $"<p><a href = {ResetPasswordUrl} >Reset Password</a><br /><br />" +
                "<p><i>Ignore this email if you did not do it. This email is generated by a machine, you do not need to reply.</i></p>");

            // Now we just need to set the message body and we're done
            message.Body = builder.ToMessageBody();

            return message;

        }

    }
}
