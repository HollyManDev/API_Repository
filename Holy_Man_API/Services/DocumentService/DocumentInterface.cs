using Holy_Man_API.Models;
using Holy_Man_API.ServerResponse;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Holy_Man_API.Services.DocumentService
{
    public interface DocumentInterface
    {
        Task<ServiceResponse<DocumentModel>> UploadDocument(IFormFile file, int id, int userId);
        Task<ServiceResponse<DocumentModel>> GetDocument(int id);
        Task<ServiceResponse<FileStream>> DownloadDocument(int id);
    }
}
