using Microsoft.AspNetCore.Http;

namespace SYS.Invoice.BLL.Models
{
    public class FileRequest
    {
        public IFormFile File { get; set; }
    }
}