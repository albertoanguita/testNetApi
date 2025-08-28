using System.Net;
using System.Net.Mail;

namespace TestWebAPI1.notifications;

public static class EmailSender
{
    public static void SendEmail(string sender, string senderName, string password, string recipient, string recipientName, string subject, string body)
    {
        var fromAddress = new MailAddress(sender, senderName);
        var toAddress = new MailAddress(recipient, recipientName);

        var smtp = new SmtpClient
        {
            Host = "smtp.gmail.com",
            Port = 587,
            EnableSsl = true,
            DeliveryMethod = SmtpDeliveryMethod.Network,
            UseDefaultCredentials = false,
            Credentials = new NetworkCredential(fromAddress.Address, password)
        };
        using (var message = new MailMessage(fromAddress, toAddress)
               {
                   Subject = subject,
                   Body = body
               })
        {
            smtp.Send(message);
        }
    }
}