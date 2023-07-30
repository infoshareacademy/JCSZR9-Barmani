using DrinkItUpBusinessLogic.MailKitSender;
using MimeKit;


namespace DrinkItUpBusinessLogic.Interfaces
{
    public interface IEmailService
    {
        public void SendEmail(Message message);
        public MimeMessage CreateEmailMessage(Message message);
        public void Send(MimeMessage mailMessage);
    }
}

