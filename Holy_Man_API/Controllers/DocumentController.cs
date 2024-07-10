using Holy_Man_API.Models;
using Holy_Man_API.ModelsView;
using Holy_Man_API.ServerResponse;
using Holy_Man_API.Services.DocumentService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Holy_Man_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        private readonly DocumentInterface _documentService;

        public DocumentController(DocumentInterface documentService)
        {
            _documentService = documentService;
        }

        [HttpPost("upload")]
        public async Task<ActionResult<ServiceResponse<DocumentModel>>> UploadDocument([FromForm] DocumentView documentView)
        {
            if (documentView.File == null || documentView.File.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }

            var serviceResponse = await _documentService.UploadDocument(documentView.File, documentView.ConversationId, documentView.UserId);
            if (!serviceResponse.Success)
            {
                return BadRequest(serviceResponse);
            }

            return Ok(serviceResponse);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<DocumentModel>>> GetDocument(int id)
        {
            var serviceResponse = await _documentService.GetDocument(id);
            if (!serviceResponse.Success)
            {
                return NotFound(serviceResponse);
            }

            return Ok(serviceResponse);
        }
        [HttpGet("download/{id}")]
        public async Task<IActionResult> DownloadDocument(int id)
        {
            var serviceResponse = await _documentService.DownloadDocument(id);

            if (!serviceResponse.Success)
            {
                return NotFound(serviceResponse);
            }

            var fileStream = serviceResponse.Data;
            return File(fileStream, "application/octet-stream", "downloaded_document.txt"); // Aqui você pode ajustar o nome do arquivo conforme necessário
        }
    }
}

