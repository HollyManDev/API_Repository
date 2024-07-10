using Holy_Man_API.Models;
using System.Collections.Generic;

namespace Holy_Man_API.ModelsView
{
    public class UserContent
    {
        public int UserId { get; set; }
        public int ParticipantId { get; set; }
        public int ConversationId { get; set; }

        public List<MessageModel> Messages { get; set; }
        public List<DocumentModel> Documents { get; set; }
        
    }
}
