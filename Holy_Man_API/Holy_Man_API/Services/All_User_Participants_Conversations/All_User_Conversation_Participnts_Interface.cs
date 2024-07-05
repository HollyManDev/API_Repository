using Holy_Man_API.ModelsView;
using Holy_Man_API.ServerResponse;
using System.Threading.Tasks;

namespace Holy_Man_API.Services.All_User_Participants_Conversations
{
    public interface All_User_Conversation_Participnts_Interface
    {
        Task<ServiceResponse<AllUserConversationParticipants>> GetConversationsWithParticipants(int userId);
    }
}
