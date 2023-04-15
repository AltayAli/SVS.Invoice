using Microsoft.AspNetCore.Http;
using SYS.Invoice.BLL.Models;
using System.Threading.Tasks;

namespace SYS.Invoice.BLL.HelperServices
{
    public interface IFileOperationsService
    {
        Task<FileDetailModel> Upload(IFormFile file);
    }
}