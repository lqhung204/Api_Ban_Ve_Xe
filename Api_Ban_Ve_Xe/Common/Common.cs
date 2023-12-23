using System.Net;
using System.Net.Mail;

namespace Api_Ban_Ve_Xe.Common
{
    public class Common
    {
        private static string password = "najymodwfaneetbh";
        private static string email = "lqhung420it@gmail.com";
        public static bool SendMail(string name, string subject, string content, string toMail, string attachmentPath)
        {
            bool rs = false;
            try
            {
                MailMessage message = new MailMessage();
                var smtp = new System.Net.Mail.SmtpClient();
                {
                    smtp.Host = "smtp.gmail.com";
                    smtp.Port = 587;
                    smtp.EnableSsl = true;
                    smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential()
                    {
                        UserName = email,
                        Password = password
                    };
                }
                MailAddress formAddress = new MailAddress(email, name);
                message.From = formAddress;
                message.To.Add(toMail);
                message.Subject = subject;
                message.IsBodyHtml = true;
                message.Body = content;

                // Đính kèm tệp PDF
                Attachment attachment = new Attachment(attachmentPath);
                message.Attachments.Add(attachment);

                smtp.Send(message);
                rs = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Data.ToString());
                rs = false;
            }
            return rs;
        }
    }
}
