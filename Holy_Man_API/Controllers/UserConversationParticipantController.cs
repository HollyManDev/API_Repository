using Holy_Man_API.ModelsView;
using Holy_Man_API.ServerResponse;
using Holy_Man_API.Services.All_User_Participants_Conversations;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Holy_Man_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserConversationParticipantController : ControllerBase
    {
        private readonly All_User_Conversation_Participnts_Services _allUserConversationService;

        public UserConversationParticipantController(All_User_Conversation_Participnts_Services allUserConversationService)
        {
            _allUserConversationService = allUserConversationService;
        }

        [HttpGet("User/{userId}/ConversationsWithParticipants")]
        public async Task<ActionResult<ServiceResponse<AllUserConversationParticipants>>> GetConversationsWithParticipants(int userId)
        {
            var serviceResponse = await _allUserConversationService.GetConversationsWithParticipants(userId);

            if (!serviceResponse.Success)
            {
                return NotFound(serviceResponse);
            }

            return Ok(serviceResponse);
        }
    }
}
