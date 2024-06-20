using Holy_Man_API.Models;
using Holy_Man_API.ModelsView;
using Holy_Man_API.ServerResponse;
using Holy_Man_API.Services.ConversationParticipants;
using Holy_Man_API.Services.MessageService;
using Holy_Man_API.Services.UserServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Holy_Man_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParticipantsController : ControllerBase
    {
        private readonly ConversationParticipantsInterface _ParticipantInterface;
        public ParticipantsController(ConversationParticipantsInterface PInterface)
        {
            _ParticipantInterface = PInterface;
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<ConversationParticipantsModel>>>> AddConversationParticipants(ConversationParticipantsView newParticipant)
        {
            var serviceResponse = await _ParticipantInterface.AddConversationParticipants(newParticipant);
            return Ok(serviceResponse);
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<ConversationParticipantsModel>>>> GetConversationsParticipants()
        {
            return Ok(await _ParticipantInterface.GetConversationsParticipants());
        }

        //[HttpPut]
        //public async Task<ActionResult<ServiceResponse<List<ConversationParticipantsModel>>>> UpdateConversationParticipants(ConversationParticipantsView updatedParticipant)
        //{
        //    var serviceResponse = await _ParticipantInterface.UpdateConversationParticipants(updatedParticipant);
        //    return Ok(serviceResponse);
        //}

        [HttpGet("{id} Search")]
        public async Task<ActionResult<ServiceResponse<ConversationParticipantsModel>>> GetConversationParticipants(int id)
        {
            ServiceResponse<ConversationParticipantsModel> serviceResponse = await _ParticipantInterface.GetConversationParticipants(id);

            return Ok(serviceResponse);

        }

        [HttpPut("Delete Participant")]
        public async Task<ActionResult<ServiceResponse<List<ConversationParticipantsModel>>>> DeleteConversationParticipants(int id)
        {
            ServiceResponse<List<ConversationParticipantsModel>> serviceResponse = await _ParticipantInterface.DeleteConversationParticipants(id);

            return Ok(serviceResponse);

        }
       

      
    }
       
}
