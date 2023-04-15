using System.ComponentModel.DataAnnotations;

namespace SYS.Invoice.BLL.Models
{
    public class FileDetailModel
    {
        [StringLength(256)]
        public string Name { get; set; }
        public string Text { get; set; }
    }
}