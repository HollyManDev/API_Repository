using Holy_Man_API.ModelsView;
using Holy_Man_API.Models;
using Holy_Man_API.ServerResponse;
using Holy_Man_API.Services.ConversationService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Holy_Man_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConversationController : ControllerBase
    {
        private readonly ConversationInterface _conversationService;

        public ConversationController(ConversationInterface conversationService)
        {
            _conversationService = conversationService;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<ConversationModel>>>> GetConversations()
        {
            var serviceResponse = await _conversationService.GetConversations();
            return Ok(serviceResponse);
        }

        [HttpGet("{id} Search")]
        public async Task<ActionResult<ServiceResponse<ConversationModel>>> GetConversation(int id)
        {
            var serviceResponse = await _conversationService.GetConversation(id);

            if (!serviceResponse.Success)
            {
                return NotFound(serviceResponse);
            }

            return Ok(serviceResponse);
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<ConversationModel>>>> CreateConversation(ConversationView newConversation)
        {
            var serviceResponse = await _conversationService.CreateConversation(newConversation);
            return Ok(serviceResponse);
        }

        [HttpPut ("Update")]
        public async Task<ActionResult<ServiceResponse<List<ConversationModel>>>> UpdateConversation(ConversationView updatedConversation)
        {
            var serviceResponse = await _conversationService.UpdateConversation(updatedConversation);
            return Ok(serviceResponse);
        }

        [HttpPut("{id} Delete")]
        public async Task<ActionResult<ServiceResponse<List<ConversationModel>>>> DeleteConversation(int id)
        {
            var serviceResponse = await _conversationService.DeleteConversation(id);
            return Ok(serviceResponse);
        }
    }
}
