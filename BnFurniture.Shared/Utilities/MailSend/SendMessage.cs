using System.Net.Mail;
using System.Net;
using Microsoft.AspNetCore.Identity;
using System.Text.Json;

namespace ASP_Work.Services.MailSend
{
    public class SendMessage:IMailServices
    {
        static public dynamic? mailconfig;
        public void SendMess(string newPassword, string login)
        {
            mailconfig = JsonSerializer.Deserialize<dynamic>(File.ReadAllText("emailconfig.json"));

            if (mailconfig is null) 
            {
                Console.WriteLine("Email configuration load error");
                return;
            }

            String? mailto = login; 
            DateTime now = DateTime.Now;
            String subject = "Смена Пароля";
            String emailBody = "Пройдите повторную аутентификацию с новым паролем ";
            emailBody += "\nUser: " + login;
            emailBody += "\nВаш новый пароль:" + newPassword;
            emailBody += "\ntime " + now.ToString();
            
            if (mailconfig is null) return;
            JsonElement smtp = mailconfig.GetProperty("smtp");
            String host = smtp.GetProperty("host").GetString()!;
            int port = smtp.GetProperty("port").GetInt32();
            String mailbox = smtp.GetProperty("email").GetString()!;
            String password = smtp.GetProperty("password").GetString()!;
            bool ssl = smtp.GetProperty("ssl").GetBoolean();

            using var smtpClient = new SmtpClient(host)
            {
                Port = port,
                EnableSsl = ssl,
                Credentials = new NetworkCredential(mailbox, password)
            };
            smtpClient.Send(mailbox, mailto, subject, emailBody);
            Console.WriteLine("Сообщение отправлено");
           
        }
    }
}
