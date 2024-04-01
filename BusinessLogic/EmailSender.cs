using System.Net;
using System.Net.Mail;

public static class EmailSender
{
    public static void SendResetEmail(string toAddress, string token)
    {
        // Set up SMTP client
        using (SmtpClient client = new SmtpClient("smtp.example.com"))
        {
            client.Port = 587;
            client.Credentials = new NetworkCredential("your-email@example.com", "your-password");
            client.EnableSsl = true;

            // Compose email message
            MailMessage message = new MailMessage();
            message.From = new MailAddress("your-email@example.com");
            message.To.Add(toAddress);
            message.Subject = "Password Reset";
            message.Body = $"Please click the following link to reset your password: https://yourwebsite.com/reset?token={token}";

            // Send the email
            client.Send(message);
        }
    }
}
