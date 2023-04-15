namespace SYS.Invoice.BLL.Models
{
    public class MailSettings
    {
        public string SenderMail { get; set; }
        public string SenderDisplayName { get; set; }
        public string SenderPassword { get; set; }
        public string SendMail { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
    }
}