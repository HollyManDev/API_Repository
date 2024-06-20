using Holy_Man_API.Models;
using Holy_Man_API.ModelsView;
using Holy_Man_API.ServerResponse;

namespace Holy_Man_API.Services.DocumentService
{
    public interface DocumentInterface
    {
        Task<ServiceResponse<DocumentModel>> UploadDocument(IFormFile file);
        Task<ServiceResponse<DocumentModel>> GetDocument(int id);
    }
}
