
namespace DrinkItUpBusinessLogic.MailKitSender
{
    public class Message
    {
        public string To { get; set; }
        public string Subject { get; set; }
        public string MessageContent { get; set; }

        public Message(string to, string subject, string messageContent)
        {
            To = to;
            Subject = subject;
            MessageContent = messageContent;
        }
    }
}
