namespace ASP_Work.Services.MailSend
{
    public interface IMailServices
    {
        public void SendMess(string newPassword, string login);
    }
}
