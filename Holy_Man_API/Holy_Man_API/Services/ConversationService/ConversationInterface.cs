using Holy_Man_API.Models;
using Holy_Man_API.ModelsView;
using Holy_Man_API.ServerResponse;

namespace Holy_Man_API.Services.ConversationService
{
    public interface ConversationInterface
    {
        Task<ServiceResponse<List<ConversationModel>>> GetConversations();
        Task<ServiceResponse<ConversationModel>> GetConversation(int id);
        Task<ServiceResponse<int>> CreateConversation(ConversationView newConversation);
        Task<ServiceResponse<List<ConversationModel>>> UpdateConversation(ConversationView updatedConversation);
        Task<ServiceResponse<List<ConversationModel>>> DeleteConversation(int id);
    }
}
