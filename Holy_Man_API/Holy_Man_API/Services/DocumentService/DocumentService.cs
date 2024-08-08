using Holy_Man_API.Models;
using Holy_Man_API.ServerResponse;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Holy_Man_API.Services.DocumentService
{
    public class DocumentService : DocumentInterface
    {
        private readonly ApplicationDbContext _context;

        public DocumentService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<ServiceResponse<DocumentModel>> UploadDocument(IFormFile file, int conversationId, int userId)
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
                    ConversationId = conversationId,
                    status = true,
                    UserId = userId,
                    doawloaded = false,
                };

                var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "UploadedDocuments");

                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                var filePath = Path.Combine(directoryPath, document.FileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                document.FilePath = filePath;

                _context.Documents.Add(document);
                await _context.SaveChangesAsync();

                serviceResponse.Data = document;
                serviceResponse.menssage = "Document uploaded successfully.";
                serviceResponse.Success = true;
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
        public async Task<ServiceResponse<FileStream>> DownloadDocument(int id)
        {
            var serviceResponse = new ServiceResponse<FileStream>();

            try
            {
                var document = await _context.Documents.FirstOrDefaultAsync(d => d.Id == id);

                if (document == null)
                {
                    serviceResponse.menssage = "Document not found.";
                    serviceResponse.Success = false;
                    return serviceResponse;
                }

                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "UploadedDocuments", document.FileName);

                if (!File.Exists(filePath))
                {
                    serviceResponse.menssage = "File not found on server.";
                    serviceResponse.Success = false;
                    return serviceResponse;
                }
                else
                {
                    var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);

                    document.doawloaded = true;

                    _context.Documents.Update(document);
                    await _context.SaveChangesAsync();

                    serviceResponse.Data = fileStream;
                    serviceResponse.menssage = "Document retrieved successfully for download.";

                }
              
            }
            catch (Exception ex)
            {
                serviceResponse.menssage = $"Error retrieving document for download: {ex.Message}";
                serviceResponse.Success = false;
            }

            return serviceResponse;
        }
        public async Task<ServiceResponse<DocumentModel>> InactivateDocument(int id)
        {
            var serviceResponse = new ServiceResponse<DocumentModel>();

            try
            {
                var gotMessage = await _context.Documents.FirstOrDefaultAsync(x => x.Id == id);

                if (gotMessage == null)
                {
                    serviceResponse.Data = null;
                    serviceResponse.menssage = "Message not found!";
                    serviceResponse.Success = false;
                }
                else
                {
                    gotMessage.status = false;
                    _context.Documents.Update(gotMessage);
                    await _context.SaveChangesAsync();

                    var DeleteMessage = await _context.Documents.FirstOrDefaultAsync(x => x.Id == gotMessage.Id);

                    serviceResponse.Data = DeleteMessage;
                    serviceResponse.menssage = "Message inactivated successfully!";
                    serviceResponse.Success = true;
                }
            }
            catch (Exception ex)
            {
                serviceResponse.menssage = ex.Message;
                serviceResponse.Success = false;
            }

            return serviceResponse;
        }
    }
}
    
