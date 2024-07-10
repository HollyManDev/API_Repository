using Holy_Man_API.ModelsView;
using Holy_Man_API.ServerResponse;
using Holy_Man_API.Services.ConversationService;

namespace Holy_Man_API.Services.UserContentServices
{
    public interface UserContentInterfacecs
    {
        Task<ServiceResponse<UserContent>> GetConversationContent(int userId, int participantId);
    }
}
