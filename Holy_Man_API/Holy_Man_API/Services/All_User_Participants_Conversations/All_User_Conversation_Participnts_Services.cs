using Holy_Man_API.Models;
using Holy_Man_API.ModelsView;
using Holy_Man_API.ServerResponse;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Holy_Man_API.Services.All_User_Participants_Conversations
{
    public class All_User_Conversation_Participnts_Services : All_User_Conversation_Participnts_Interface
    {
        private readonly ApplicationDbContext _context;

        public All_User_Conversation_Participnts_Services(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<AllUserConversationParticipants>> GetConversationsWithParticipants(int userId)
        {
            var serviceResponse = new ServiceResponse<AllUserConversationParticipants>();

            try
            {
                var userContent = await (from participant in _context.ConversationParticipants
                                         where participant.UserId == userId
                                         join conversation in _context.Conversations
                                             on participant.ConversationId equals conversation.Id
                                         select new
                                         {
                                             Conversation = conversation,
                                             Participants = _context.ConversationParticipants
                                                                 .Where(cp => cp.ConversationId == conversation.Id)
                                                                 .Select(cp => cp.User)
                                                                 .ToList()
                                         })
                         .ToListAsync();

                var allConversations = userContent.Select(item => item.Conversation).ToList();
                var allParticipants = userContent.SelectMany(item => item.Participants).Distinct().ToList(); // Distinct para evitar duplicatas

                var result = new AllUserConversationParticipants
                {
                    AllConversation = allConversations,
                    AllParticipants = allParticipants
                };

                serviceResponse.Data = result;
                serviceResponse.Success = true;
            }
            catch (Exception ex)
            {
                serviceResponse.menssage = $"Error retrieving conversation content: {ex.Message}";
                serviceResponse.Success = false;
            }

            return serviceResponse;
        }
    }
}
