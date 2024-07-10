using Holy_Man_API.Models;

namespace Holy_Man_API.ModelsView
{
    public class AllUserConversationParticipants
    {
        public List<ConversationModel> AllConversation { get; set; }
        public List<UserModel> AllParticipants { get; set; }
    }
}
