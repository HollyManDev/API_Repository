using Holy_Man_API.Models;
using Holy_Man_API.ModelsView;
using Holy_Man_API.ServerResponse;
using Microsoft.EntityFrameworkCore;



namespace Holy_Man_API.Services.DocumentService
{
    public class DocumentService : DocumentInterface
    {
        private readonly ApplicationDbContext _context;

        public DocumentService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<DocumentModel>> UploadDocument(IFormFile file)
        {
            var serviceResponse = new ServiceResponse<DocumentModel>();

            try
            {
                if (file == null || file.Length == 0)
                {
                    serviceResponse.menssage = "File not selected or empty.";
                    serviceResponse.Success = false;
                    return serviceResponse;
                }

                var document = new DocumentModel
                {
                    FileName = Path.GetFileName(file.FileName),
                    UploadedAt = DateTime.Now,
                    status = true  
                };

               
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "UploadedDocuments", document.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                
                _context.Documents.Add(document);
                await _context.SaveChangesAsync();

                serviceResponse.Data = document;
                serviceResponse.menssage = "Document uploaded successfully.";
            }
            catch (Exception ex)
            {
                serviceResponse.menssage = $"Error uploading document: {ex.Message}";
                serviceResponse.Success = false;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<DocumentModel>> GetDocument(int id)
        {
            var serviceResponse = new ServiceResponse<DocumentModel>();

            try
            {
                var document = await _context.Documents.FirstOrDefaultAsync(d => d.Id == id);

                if (document == null)
                {
                    serviceResponse.menssage = "Document not found.";
                    serviceResponse.Success = false;
                    return serviceResponse;
                }

                serviceResponse.Data = document;
                serviceResponse.menssage = "Document retrieved successfully.";
            }
            catch (Exception ex)
            {
                serviceResponse.menssage = $"Error retrieving document: {ex.Message}";
                serviceResponse.Success = false;
            }

            return serviceResponse;
        }
    }
}
