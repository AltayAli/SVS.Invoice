using SYS.Invoice.BLL.Models;
using System.Threading.Tasks;

namespace SYS.Invoice.BLL.HelperServices
{
    public interface IEmailSender
    {
       Task SendEmailAsync(MailRequest mailRequest);
    }
}