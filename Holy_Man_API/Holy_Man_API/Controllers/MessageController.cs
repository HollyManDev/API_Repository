using Holy_Man_API.Models;
using Holy_Man_API.ModelsView;
using Holy_Man_API.ServerResponse;
using Holy_Man_API.Services.MessageService;
using Microsoft.AspNetCore.Mvc;

namespace Holy_Man_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly MessageInterface _messageService;

        public MessageController(MessageInterface messageService)
        {
            _messageService = messageService;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<MessageModel>>>> GetMessages()
        {
            var serviceResponse = await _messageService.GetMessages();
            return Ok(serviceResponse);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<MessageModel>>> GetMessage(int id)
        {
            var serviceResponse = await _messageService.FindMessage(id);
            return Ok(serviceResponse);
        }

        [HttpPut("Inactivate/{id}")]
        public async Task<ActionResult<ServiceResponse<List<MessageModel>>>> DeactivateMessage(int id)
        {
            var serviceResponse = await _messageService.InactivateMessage(id);
            return Ok(serviceResponse);
        }

        [HttpPut("Activate/{id}")]
        public async Task<ActionResult<ServiceResponse<List<MessageModel>>>> ActivateMessage(int id)
        {
            var serviceResponse = await _messageService.ActivateMessage(id);
            return Ok(serviceResponse);
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<List<MessageModel>>>> UpdateMessage(MessageView updatedMessage)
        {
            var serviceResponse = await _messageService.UpdateMessage(updatedMessage);
            return Ok(serviceResponse);
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<MessageModel>>>> CreateMessage(MessageView newMessage)
         {
            var serviceResponse = await _messageService.CreateMessage(newMessage);
            return Ok(serviceResponse);
        }
    }
}
