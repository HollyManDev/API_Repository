using Holy_Man_API.Models;
using Holy_Man_API.ModelsView;
using Holy_Man_API.ServerResponse;

namespace Holy_Man_API.Services.ConversationParticipants
{
    public interface ConversationParticipantsInterface
    {


        Task<ServiceResponse<List<ConversationParticipantsModel>>> GetConversationsParticipants();
        Task<ServiceResponse<ConversationParticipantsModel>> GetConversationParticipants(int id);
        Task<ServiceResponse<int>> GetConversationId(int user, int participant);
        Task<ServiceResponse<List<ConversationParticipantsModel>>> AddConversationParticipants(ConversationParticipantsView newTalkers);
        //Task<ServiceResponse<List<ConversationParticipantsModel>>> UpdateConversationParticipants(ConversationParticipantsView updatedTalkers);
        Task<ServiceResponse<List<ConversationParticipantsModel>>> DeleteConversationParticipants(int id);


    }
}
